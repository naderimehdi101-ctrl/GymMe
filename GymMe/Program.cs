using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=GymMe.db"));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddOutputCache();

builder.AddApplicationServices();

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


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.AddSwaggerAuth();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.UseExceptionHandler();

app.UseOutputCache();

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.UseResponseInterceptor(@"(httpResponse) => {
            const isSuccessfulLogin =
                httpResponse.url.endsWith('/accounts/login') &&
                httpResponse.status === 200;

            if (isSuccessfulLogin) {
                const loginResponseBody = JSON.parse(httpResponse.text);

                if (loginResponseBody.token) {
                    window.ui.preauthorizeApiKey('Bearer', loginResponseBody.token);
                }
            }
            return httpResponse;
        }");
    });
app.UseAuthentication();
app.UseAuthorization();

app.MapApplicationEndpoints();

app.Run();