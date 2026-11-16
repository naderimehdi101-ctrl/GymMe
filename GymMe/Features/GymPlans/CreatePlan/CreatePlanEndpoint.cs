namespace GymMe.Features.GymPlans.CreatePlan;

public static class CreatePlanEndpoint
{
    public static void MapCreatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/plans", async (CreatePlanRequest request, CreatePlanHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Created($"/plans/{result.Value.Id}", result.Value);
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<CreatePlanRequest>>()
        .WithTags("GymPlans");
    }
}