
namespace GymMe.Extensions;

public static class AuthorizationExtension
{
    public static WebApplicationBuilder AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Member", policy => policy.RequireRole("Member"));
            options.AddPolicy("Trainer", policy => policy.RequireRole("Trainer"));
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });
        
        return builder;
    }
}