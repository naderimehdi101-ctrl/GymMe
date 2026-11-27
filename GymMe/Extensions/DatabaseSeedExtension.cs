namespace GymMe.Extensions;

public static class DatabaseSeedExtension
{ 
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        await db.Database.EnsureCreatedAsync();

        var adminExists = await db.Users.AnyAsync(x => x.Role == Role.Admin);
        if (adminExists)
            return;

        var email = config["DefaultAdmin:Email"]?.ToLower().Trim()
            ?? throw new InvalidOperationException("Configuration error: 'DefaultAdmin:Email' is missing.");

        var password = config["DefaultAdmin:Password"]
            ?? throw new InvalidOperationException("Configuration error: 'DefaultAdmin:Password' is missing.");

        var admin = new User
        {
            Email = email,
            Role = Role.Admin
        };

        admin.PasswordHash = hasher.HashPassword(admin, password);

        db.Users.Add(admin);
        await db.SaveChangesAsync();
    }
}
