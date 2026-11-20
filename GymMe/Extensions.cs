
namespace GymMe.Extensions;

//ENDPOINTS

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(this IEndpointRouteBuilder app)
    {

        // Registration Endpoints
        app.MapRegisterAndCreateMemberEndpoint();
        app.MapRegisterTrainerEndpoint();
        app.MapLoginEndpoint();

        // Gym Plan Endpoints
        app.MapCreatePlanEndpoint();
        app.MapDeletePlanEndpoint();
        app.MapGetAllPlansEndpoint();
        app.MapGetPlanEndpoint();
        app.MapUpdatePlanEndpoint();
        app.MapMigrateAndDeletePlanEndpoint();

        // Member Endpoints
        app.MapCreateMemberEndpoint();
        app.MapDeleteMemberEndpoint();
        app.MapGetAllMembersEndpoint();
        app.MapGetMemberEndpoint();
        app.MapUpdateMemberEndpoint();

        // Membership Endpoints
        app.MapCreateMembershipEndpoint();
        app.MapGetMembershipEndpoint();

        return app;
    }
}

//DEPENDENCY INJECTION SERVICES

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<LoginHandler>();
        builder.Services.AddScoped<RegisterAndCreateMemberHandler>();
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
        builder.Services.AddScoped<MigrateAndDeletePlanHandler>();

        return builder;
    }
}

//RESULT EXTENSION

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this ResultBase result)
    {
        var errors = result.Reasons.Select(r => r.Message).ToList();

        return Results.BadRequest(new { errors });
    }
}

// SWAGGER AUTH, SWAGGER & SWAGGER UI

public static class SwaggerExtensions
{
    public static WebApplicationBuilder AddSwaggerAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT token directly"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });
        return builder;
    }

    public static WebApplication UseSwaggerAndSwaggerUI(this WebApplication app)
    {
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

        return app;
    }
}

// AUTHENTICATION & AUTHORIZATION

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