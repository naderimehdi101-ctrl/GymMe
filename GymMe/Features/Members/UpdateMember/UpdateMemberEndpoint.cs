namespace GymMe.Features.Members.UpdateMember;

public static class UpdateMemberEndpoint
{
    public static void MapUpdateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/members/{id:int}", async (int id, UpdateMemberRequest request, UpdateMemberHandler handler) =>
        {
            await handler.HandleAsync(id, request);
            return Results.Ok(new { Message = "Member successfully updated!" });
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<UpdateMemberRequest>>()
        .WithTags("Members");
    }
}