
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace GymMe.Service;

public class AccountService
{
    private readonly AppDbContext _db;
    private IPasswordHasher<User> _hasher;
    private IConfiguration _configuration;
    
    public AccountService(AppDbContext db, IPasswordHasher<User> hasher, IConfiguration configuration)
    {
        _db = db;
        _hasher = hasher;
        _configuration = configuration;
    }
    
    public async Task<UserResponse> Register(AccountDto accountDto)
    {
        var email = accountDto.Email.ToLower().Trim();    
        
        var emailExists = await _db.Users.AnyAsync(x => x.Email == email);
        if(emailExists)
            throw new InvalidOperationException("The Email already exists");
            
        var register = new User
        {
            Email = email,
        };
        
        var hashPassword = _hasher.HashPassword(register, accountDto.Password);
        
        register.PasswordHash = hashPassword;
        
        _db.Users.Add(register);
        await _db.SaveChangesAsync();
        
        return new UserResponse
        {
            ID = register.ID,
            Email = register.Email,
            Role = register.Role
        };
    }
    
    public async Task<UserResponse> RegisterTrainer(AccountDto accountDto)
    {
        var email = accountDto.Email.ToLower().Trim();    
        
        var emailExists = await _db.Users.AnyAsync(x => x.Email == email);
        if(emailExists)
            throw new InvalidOperationException("The Email already exists");
            
        var register = new User
        {
            Email = email,
            Role = Role.Trainer
        };
        
        var hashPassword = _hasher.HashPassword(register, accountDto.Password);
        
        register.PasswordHash = hashPassword;
        
        _db.Users.Add(register);
        await _db.SaveChangesAsync();
        
        return new UserResponse
        {
            ID = register.ID,
            Email = register.Email,
            Role = register.Role
        };
    }
    
    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {
        var email = loginDto.Email.ToLower().Trim();    
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        if(user is null)
            throw new InvalidOperationException("The Email already exists");
            
        var login = new User
        {
            Email = email,
        };
        
        var verifyPassword = _hasher.VerifyHashedPassword(login, login.PasswordHash, loginDto.Password);
        
        var token = GenerateJWTToken(user);
        
        return new LoginResponse
        {
            ID = login.ID,
            Token = token,
            Email = login.Email,
            Role = login.Role
        };
    }
    
    private string GenerateJWTToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if(jwtKey is null)
            throw new InvalidOperationException("Jwt key is missing");
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        }; 
        
        var token = new JwtSecurityToken(
            issuer : jwtIssuer,
            audience : jwtAudience,
            claims : claims,
            expires : DateTime.UtcNow.AddMinutes(60),
            signingCredentials : credentials 
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}