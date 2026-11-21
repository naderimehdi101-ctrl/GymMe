
namespace GymMe.Extensions;

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