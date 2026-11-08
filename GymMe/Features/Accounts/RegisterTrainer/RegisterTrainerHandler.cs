namespace GymMe.Features.Accounts.RegisterTrainer;

public sealed record RegisterTrainerRequest(string Email, string Password);
public sealed record RegisterTrainerResponse(int ID, string Email, Role Role);

public class RegisterTrainerHandler(AppDbContext db, IPasswordHasher<User> hasher)
{
    public async Task<RegisterTrainerResponse> HandleAsync(RegisterTrainerRequest request)
    {
        var email = request.Email.ToLower().Trim();

        var emailExists = await db.Users.AnyAsync(x => x.Email == email);
        if (emailExists)
            throw new InvalidOperationException("The Email already exists");

        var user = new User
        {
            Email = email,
            Role = Role.Trainer
        };

        user.PasswordHash = hasher.HashPassword(user, request.Password);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return new RegisterTrainerResponse(user.ID, user.Email, user.Role);
    }
}