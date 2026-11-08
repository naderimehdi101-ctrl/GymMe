namespace GymMe.Features.GymPlans.CreatePlan;

public static class CreatePlanEndpoint
{
    public static void MapCreatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/plans", async (CreatePlanRequest request, CreatePlanHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            return Results.Created($"/plans/{response.ID}", response);
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<CreatePlanRequest>>()
        .WithTags("GymPlans");
    }
}