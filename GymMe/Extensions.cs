
namespace GymMe.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(this IEndpointRouteBuilder app)
    {

        // Registration Endpoints
        app.MapRegisterMemberEndpoint();
        app.MapRegisterTrainerEndpoint();
        app.MapLoginEndpoint();

        // Gym Plan Endpoints
        app.MapCreatePlanEndpoint();
        app.MapDeletePlanEndpoint();
        app.MapGetAllPlansEndpoint();
        app.MapGetPlanEndpoint();
        app.MapUpdatePlanEndpoint();

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

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
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

        return builder;
    }
}

public static class ResultaExtensions
{
    public static IResult ToProblemDetails(this ResultBase result)
    {
        var errors = result.Reasons.Select(r => r.Message).ToList();
        
        return Results.BadRequest(new { errors });
    }
}

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
}