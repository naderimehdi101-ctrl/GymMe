namespace GymMe.Features.Accounts.RegisterTrainer;

public static class RegisterTrainerEndpoint
{
    public static void MapRegisterTrainerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/trainer", async (RegisterTrainerRequest request, RegisterTrainerHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Created($"/accounts/trainer/{result.Value.ID}", result.Value);
        })
        .AddEndpointFilter<ValidationFilter<RegisterTrainerRequest>>()
        .RequireAuthorization()
        .WithTags("Registration");
    }
}