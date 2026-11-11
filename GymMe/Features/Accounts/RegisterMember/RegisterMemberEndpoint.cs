namespace GymMe.Features.Accounts.RegisterMember;

public static class RegisterMemberEndpoint
{
    public static void MapRegisterMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/member", async (RegisterMemberRequest request, RegisterMemberHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Created($"/accounts/member/{result.Value.ID}", result.Value);
        })
        .AddEndpointFilter<ValidationFilter<RegisterMemberRequest>>()
        .WithTags("Registration");
    }
}