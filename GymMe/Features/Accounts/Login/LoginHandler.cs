
using System.IdentityModel.Tokens.Jwt;

namespace GymMe.Features.Accounts.Login;

public sealed record LoginRequest(string Email, string Password);
public sealed record LoginResponse(int Id, string Token, string Email, Role Role);

public class LoginHandler(AppDbContext db, IPasswordHasher<User> hasher, IConfiguration configuration)
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var email = request.Email.ToLower().Trim();
        
        var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return Result.Fail<LoginResponse>("Invalid email or password");

        var verifyPassword = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verifyPassword == PasswordVerificationResult.Failed)
            return Result.Fail<LoginResponse>("Invalid email or password");

        var token = GenerateJWTToken(user);

        return Result.Ok(new LoginResponse(user.Id, token, email , user.Role));
    }

    private string GenerateJWTToken(User user)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new ("Jwt key is missing");
        var jwtIssuer = configuration["Jwt:Issuer"];
        var jwtAudience = configuration["Jwt:Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
