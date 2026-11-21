
namespace GymMe.Extensions;

public static class AuthenticationAndAuthorization
{
    public static WebApplicationBuilder AddAuthenticationAndAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Member", policy => policy.RequireRole("Member"));
            options.AddPolicy("Trainer", policy => policy.RequireRole("Trainer"));
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        return builder;
    }
}