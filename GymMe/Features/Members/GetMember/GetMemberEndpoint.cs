namespace GymMe.Features.Members.GetMember;

public static class GetMemberEndpoint
{
    public static void MapGetMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/members/{id:int}", async (int id, GetMemberHandler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            return Results.Ok(result.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)))
        .WithTags("Members");
    }
}