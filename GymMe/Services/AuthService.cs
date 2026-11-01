
using GymMe;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly AppDbContext _db;
    private IPasswordHasher<User> _hasher;
    
    public AuthService(AppDbContext db, IPasswordHasher<User> hasher)
    {
        _db = db;
        _hasher = hasher;
    }
    
    public async Task<UserResponse> Register(RegisterDto dto)
    {
        var email = dto.Email.ToLower().Trim();    
        
        var emailExists = await _db.Users.AnyAsync(x => x.Email == email);
        if(emailExists)
            throw new InvalidOperationException("The Email already exists");
            
        var register = new User
        {
            Email = email
        };
        
        var hashPassword = _hasher.HashPassword(register, dto.Password);
        
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
}