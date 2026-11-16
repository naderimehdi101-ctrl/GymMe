namespace GymMe.Features.Members.CreateMember;

public static class CreateMemberEndpoint
{
    public static void MapCreateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/members", async (CreateMemberRequest request, CreateMemberHandler handler) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Created($"/members/{result.Value.Id}", result.Value);
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<CreateMemberRequest>>()
        .WithTags("Members");
    }
}