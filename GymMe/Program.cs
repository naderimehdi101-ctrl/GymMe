// P R O G R A M . C S //

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=GymMe.db"));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddOutputCache();

builder.AddApplicationServices();

builder.AddAuthenticationAndAuthorization();

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

app.UseSwaggerAndSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapApplicationEndpoints();

app.Run();