namespace GymMe.Features.GymPlans.UpdatePlan;

public static class UpdatePlanEndpoint
{
    public static void MapUpdatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/plans/{id:int}", async (int id, UpdatePlanRequest request, UpdatePlanHandler handler) =>
        {
            var result = await handler.HandleAsync(id, request);
            if(result.IsFailed)
                return result.ToProblemDetails();
            
            return Results.Ok(new { Message = "Plan successfully updated!" });
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<UpdatePlanRequest>>()
        .WithTags("GymPlans");
    }
}