namespace GymMe.Features.Memberships.GetMembership;

public static class GetMembershipEndpoint
{
    public static void MapGetMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/memberships/{id:int}", async (int id, GetMembershipHandler handler) =>
        {
            var response = await handler.HandleAsync(id);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithTags("Memberships");
    }
}