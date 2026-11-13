namespace GymMe.Features.GymPlans.CreatePlan;

public static class CreatePlanEndpoint
{
    public static void MapCreatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/plans", async (CreatePlanRequest request, CreatePlanHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            if(response.IsFailed)
                return response.ToProblemDetails();
                
            return Results.Created($"/plans/{response.Value.ID}", response.Value);
        })
        .RequireAuthorization("MemberAccess")
        .AddEndpointFilter<ValidationFilter<CreatePlanRequest>>()
        .WithTags("GymPlans");
    }
}