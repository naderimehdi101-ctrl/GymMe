namespace GymMe.Features.Accounts.RegisterMember;

public static class RegisterMemberEndpoint
{
    public static void MapRegisterMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/member", async (RegisterMemberRequest request, RegisterMemberHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            return Results.Created($"/accounts/member/{result.ID}", result);
        })
        .AddEndpointFilter<ValidationFilter<RegisterMemberRequest>>()
        .WithTags("Registration");
    }
}