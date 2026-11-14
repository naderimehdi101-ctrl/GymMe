namespace GymMe.Features.GymPlans.GetPlan;

public static class GetPlanEndpoint
{
    public static void MapGetPlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/plans/{id:int}", async (int id, GetPlanHandler handler) =>
        {
            var response = await handler.HandleAsync(id);
            if(response.IsFailed)
                return response.ToProblemDetails();
                
            return Results.Ok(response.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)))
        .WithTags("GymPlans");
    }
}