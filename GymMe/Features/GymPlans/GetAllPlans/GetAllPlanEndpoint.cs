
namespace GymMe.Features.GymPlans.GetAllPlans;

public static class GetAllPlansEndpoint
{
    public static void MapGetAllPlansEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/plans", async
        (
            GetAllPlansHandler handler,
            CancellationToken ct,
            int? lastId = null, 
            int pageSize = 20
            ) =>
        {
            var result = await handler.HandleAsync(lastId, pageSize, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(result.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)).Tag(CacheTags.Plans))
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .WithTags("GymPlans");
    }
}