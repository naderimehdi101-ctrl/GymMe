namespace GymMe.Features.MyProfile.GetMyProfile;

public static class GetMyProfileEndpoint
{
    public static void MapGetMyProfileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members/me", async (
            ClaimsPrincipal user,
            GetMyProfileHandler handler,
            CancellationToken ct) =>
        {
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var result = await handler.HandleAsync(userId, ct);
            if (result.IsFailed)
                return result.ToProblemDetails();

            return Results.Ok(result.Value);
        })
        .RequireAuthorization("Member")
        .RequireRateLimiting(RateLimitingPolicies.ApiStandard)
        .WithTags("MyProfile");
    }
}
