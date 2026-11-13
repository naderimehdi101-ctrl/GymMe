namespace GymMe.Features.Members.DeleteMember;

public static class DeleteMemberEndpoint
{
    public static void MapDeleteMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/members/{id:int}", async (int id, DeleteMemberHandler handler) =>
        {
            var result = await handler.HandleAsync(id);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(new { Message = "Member successfully removed!" });
        })
        .RequireAuthorization("Admin")
        .WithTags("Members");
    }
}