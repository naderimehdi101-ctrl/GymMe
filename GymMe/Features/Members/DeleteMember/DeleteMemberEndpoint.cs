namespace GymMe.Features.Members.DeleteMember;

public static class DeleteMemberEndpoint
{
    public static void MapDeleteMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/members/{id:int}", async (int id, DeleteMemberHandler handler) =>
        {
            await handler.HandleAsync(id);
            return Results.Ok(new { Message = "Member successfully removed!" });
        })
        .RequireAuthorization()
        .WithTags("Members");
    }
}