namespace GymMe.Features.Members.CreateMember;

public static class CreateMemberEndpoint
{
    public static void MapCreateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/members", async (CreateMemberRequest request, CreateMemberHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            return Results.Created($"/members/{response.ID}", response);
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<CreateMemberRequest>>()
        .WithTags("Members");
    }
}