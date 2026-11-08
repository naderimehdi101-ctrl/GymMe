namespace GymMe.Features.Members.GetMember;

public static class GetMemberEndpoint
{
    public static void MapGetMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members/{id:int}", async (int id, GetMemberHandler handler) =>
        {
            var response = await handler.HandleAsync(id);
            return Results.Ok(response);
        })
        .WithTags("Members");
    }
}