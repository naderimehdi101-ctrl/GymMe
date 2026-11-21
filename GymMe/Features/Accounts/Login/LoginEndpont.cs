using GymMe.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace GymMe.Features.Accounts.Login;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/login", async (LoginRequest request, LoginHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(result.Value);
        })
        .RequireRateLimiting(RateLimitingPolicies.AuthStrict)
        .WithTags("Registration");
    }
}