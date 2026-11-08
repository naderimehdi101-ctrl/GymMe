namespace GymMe.Features.Accounts.Login;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/login", async (LoginRequest request, LoginHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            return Results.Ok(response);
        })
        .WithTags("Registration");
    }
}