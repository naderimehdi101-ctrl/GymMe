namespace GymMe.Features.Memberships.GetMembership;

public static class GetMembershipEndpoint
{
    public static void MapGetMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/memberships/{id:int}", async 
        (
            int id, 
            GetMembershipHandler handler, 
            CancellationToken ct
            ) =>
        {
            var result = await handler.HandleAsync(id, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(result.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)).Tag(CacheTags.Memberships))
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .RequireAuthorization()
        .WithTags("Memberships");
    }
}