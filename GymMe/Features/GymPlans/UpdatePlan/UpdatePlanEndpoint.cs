namespace GymMe.Features.GymPlans.UpdatePlan;

public static class UpdatePlanEndpoint
{
    public static void MapUpdatePlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/plans/{id:int}", async 
        (
            int id, 
            UpdatePlanRequest request, 
            UpdatePlanHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct 
            ) =>
        {
            var result = await handler.HandleAsync(id, request, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
            
            await cache.EvictByTagsAsync(ct, CacheTags.Plans, CacheTags.Members, CacheTags.Memberships);
            
            return Results.Ok(new { Message = "Plan successfully updated!" });
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<UpdatePlanRequest>>()
        .WithTags("GymPlans");
    }
}