namespace GymMe.Features.GymPlans.GetPlan;

public static class GetPlanEndpoint
{
    public static void MapGetPlanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/plans/{id:int}", async
        (
            int id,
            GetPlanHandler handler,
            CancellationToken ct
            ) =>
        {
            var result = await handler.HandleAsync(id, ct);
            if (result.IsFailed)
                return result.ToProblemDetails();

            return Results.Ok(result.Value);
        })
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)).Tag(CacheTags.Plans))
        .WithTags("GymPlans");
    }
}