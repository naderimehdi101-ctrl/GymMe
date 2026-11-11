namespace GymMe.Features.Members.UpdateMember;

public static class UpdateMemberEndpoint
{
    public static void MapUpdateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/members/{id:int}", async (int id, UpdateMemberRequest request, UpdateMemberHandler handler) =>
        {
            var result = await handler.HandleAsync(id, request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(new { Message = "Member successfully updated!" });
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<UpdateMemberRequest>>()
        .WithTags("Members");
    }
}