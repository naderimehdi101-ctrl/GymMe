namespace GymMe.Features.GymPlans.MigrateAndDeletePlan;

public static class MigrateAndDeletePlanEndpoint
{
    public static void MapMigrateAndDeletePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/plans/{oldPlanId:int}/migrate/{targetPlanId:int}",
            async (int oldPlanId, int targetPlanId, MigrateAndDeletePlanHandler handler) =>
            {
                var result = await handler.HandleAsync(oldPlanId, targetPlanId);
                if (result.IsFailed)
                    return result.ToProblemDetails();

                return Results.NoContent();
            })
            .RequireAuthorization("Admin")
            .WithTags("GymPlans");
    }
}
