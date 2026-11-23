using GymMe.RateLimiting;

namespace GymMe.Features.Members.GetAllMembers;

public static class GetAllMembersEndpoint
{
    public static void MapGetAllMembersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members", async 
        (
            GetAllMembersHandler handler,
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
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)).Tag(CacheTags.Members))
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .WithTags("Members");
    }
}