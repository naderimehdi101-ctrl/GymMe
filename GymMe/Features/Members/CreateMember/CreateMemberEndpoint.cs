namespace GymMe.Features.Members.CreateMember;

public static class CreateMemberEndpoint
{
    public static void MapCreateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/members", async (CreateMemberRequest request, CreateMemberHandler handler) =>
        {
            var response = await handler.HandleAsync(request);
            if(response.IsFailed)
                return response.ToProblemDetails();
                
            return Results.Created($"/members/{response.Value.ID}", response.Value);
        })
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<CreateMemberRequest>>()
        .WithTags("Members");
    }
}