using Microsoft.AspNetCore.Http.HttpResults;

namespace GymMe.Features.Accounts.RegisterMember;

public sealed record RegisterMemberRequest(string Email, string Password);
public sealed record RegisterMemberResponse(int ID, string Email, Role Role);

public class RegisterMemberHandler(AppDbContext db, IPasswordHasher<User> hasher)
{
    public async Task<Result<RegisterMemberResponse>> HandleAsync(RegisterMemberRequest request)
    {
        var email = request.Email.ToLower().Trim();

        var emailExists = await db.Users.AnyAsync(x => x.Email == email);
        if (emailExists)
            return Result.Fail<RegisterMemberResponse>("The Email already exists!");

        var user = new User
        {
            Email = email,
            Role = Role.Member
        };

        user.PasswordHash = hasher.HashPassword(user, request.Password);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Result.Ok(new RegisterMemberResponse(user.ID, user.Email, user.Role));
    }
}