// P R O G R A M . C S //

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=GymMe.db"));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddOutputCache();
builder.Services.AddGymMeRateLimiting(builder.Configuration);

builder.AddApplicationServices();

builder.AddAuthentication();
builder.AddAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.AddSwaggerAuth();

var app = builder.Build();

// Database Creation
await app.SeedDatabaseAsync();

app.UseExceptionHandler();

app.UseOutputCache();

app.UseSwaggerAndSwaggerUI();

app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapAppEndpoints();

app.Run();