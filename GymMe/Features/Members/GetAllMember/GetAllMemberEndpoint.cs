using GymMe.RateLimiting;

namespace GymMe.Features.Members.GetAllMembers;

public static class GetAllMembersEndpoint
{
    public static void MapGetAllMembersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members", async (GetAllMembersHandler handler, int page = 1, int pageSize = 10) =>
        {
            var result = await handler.HandleAsync(page, pageSize);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(result.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)))
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .WithTags("Members");
    }
}