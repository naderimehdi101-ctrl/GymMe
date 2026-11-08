namespace GymMe.Features.Members.GetAllMembers;

public static class GetAllMembersEndpoint
{
    public static void MapGetAllMembersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members", async (GetAllMembersHandler handler, int page = 1, int pageSize = 10) =>
        {
            var response = await handler.HandleAsync(page, pageSize);
            return Results.Ok(response);
        })
        .WithTags("Members");
    }
}