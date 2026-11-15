


namespace GymMe.Features.Memberships.CreateMembership;

public static class CreateMembershipEndpoint
{
    public static void MapCreateMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/memberships", async (CreateMembershipRequest request, CreateMembershipHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Created($"/memberships/{result.Value.ID}", result.Value);
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<CreateMembershipRequest>>()
        .WithTags("Memberships");
    }
}