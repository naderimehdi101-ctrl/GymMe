
namespace GymMe.Extensions;

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