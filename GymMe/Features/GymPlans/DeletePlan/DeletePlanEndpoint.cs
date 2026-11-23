namespace GymMe.Features.GymPlans.DeletePlan;

public static class DeletePlanEndpoint
{
    public static void MapDeletePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/plans/{id:int}", async 
        (
            int id, 
            DeletePlanHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct 
            ) =>
        {
            var result = await handler.HandleAsync(id);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagAsync(CacheTags.Plans, ct);    
                
            return Results.Ok(new { Message = "Plan successfully removed!" });
        })
        .RequireAuthorization("Admin")
        .WithTags("GymPlans");
    }
}