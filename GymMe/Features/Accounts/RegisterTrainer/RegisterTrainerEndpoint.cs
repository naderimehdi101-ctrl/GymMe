namespace GymMe.Features.Accounts.RegisterTrainer;

public static class RegisterTrainerEndpoint
{
    public static void MapRegisterTrainerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/trainer", async (RegisterTrainerRequest request, RegisterTrainerHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            return Results.Created($"/accounts/trainer/{result.ID}", result);
        })
        .AddEndpointFilter<ValidationFilter<RegisterTrainerRequest>>()
        .WithTags("Registration");
    }
}