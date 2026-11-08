
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
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
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Database"));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterMemberHandler>();
builder.Services.AddScoped<RegisterTrainerHandler>();
builder.Services.AddScoped<CreatePlanHandler>();
builder.Services.AddScoped<DeletePlanHandler>();
builder.Services.AddScoped<GetAllPlansHandler>();
builder.Services.AddScoped<GetPlanHandler>();
builder.Services.AddScoped<UpdatePlanHandler>();
builder.Services.AddScoped<CreateMemberHandler>();
builder.Services.AddScoped<DeleteMemberHandler>();
builder.Services.AddScoped<GetAllMembersHandler>();
builder.Services.AddScoped<GetMemberHandler>();
builder.Services.AddScoped<UpdateMemberHandler>();
builder.Services.AddScoped<CreateMembershipHandler>();
builder.Services.AddScoped<GetMembershipHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

var group = app.MapGroup("API");

// Registration Endpoints
group.MapRegisterMemberEndpoint();
group.MapRegisterTrainerEndpoint();
group.MapLoginEndpoint();

// Gym Plan Endpoints
group.MapCreatePlanEndpoint();
group.MapDeletePlanEndpoint();
group.MapGetAllPlansEndpoint();
group.MapGetPlanEndpoint();
group.MapUpdatePlanEndpoint();

// Member Endpoints
group.MapCreateMemberEndpoint();
group.MapDeleteMemberEndpoint();
group.MapGetAllMembersEndpoint();
group.MapGetMemberEndpoint();
group.MapUpdateMemberEndpoint();

// Membership Endpoints
group.MapCreateMembershipEndpoint();
group.MapGetMembershipEndpoint();

app.Run();

