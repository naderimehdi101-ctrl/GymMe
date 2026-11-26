namespace GymMe.Features.Members.DeleteMember;

public static class DeleteMemberEndpoint
{
    public static void MapDeleteMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/members/{id:int}", async 
        (
            int id, 
            DeleteMemberHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct 
            ) =>
        {
            var result = await handler.HandleAsync(id, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagAsync(CacheTags.Members, ct);    
                
            return Results.Ok(new { Message = "Member successfully removed!" });
        })
        .RequireAuthorization("Admin")
        .WithTags("Members");
    }
}