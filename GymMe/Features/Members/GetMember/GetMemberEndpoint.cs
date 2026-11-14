namespace GymMe.Features.Members.GetMember;

public static class GetMemberEndpoint
{
    public static void MapGetMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members/{id:int}", async (int id, GetMemberHandler handler) =>
        {
            var response = await handler.HandleAsync(id);
            if(response.IsFailed)
                return response.ToProblemDetails();
                
            return Results.Ok(response.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)))
        .WithTags("Members");
    }
}