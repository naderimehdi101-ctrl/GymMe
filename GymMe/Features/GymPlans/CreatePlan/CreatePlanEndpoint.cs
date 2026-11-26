namespace GymMe.Features.GymPlans.CreatePlan;

public static class CreatePlanEndpoint
{
    public static void MapCreatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/plans", async 
        (
            CreatePlanRequest request, 
            CreatePlanHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct 
            ) =>
        {
            var result = await handler.HandleAsync(request, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagAsync(CacheTags.Plans, ct);    
                
            return Results.Created($"/plans/{result.Value.Id}", result.Value);
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<CreatePlanRequest>>()
        .WithTags("GymPlans");
    }
}