namespace GymMe.Features.Memberships.CreateMembership;

public static class CreateMembershipEndpoint
{
    public static void MapCreateMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/memberships", async (CreateMembershipRequest request, CreateMembershipHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            return Results.Created($"/memberships/{response.ID}", response);
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<CreateMembershipRequest>>()
        .WithTags("Memberships");
    }
}