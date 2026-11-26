namespace GymMe.Features.Membership.DeleteMembership;

public static class DeleteMembershipEndpoint
{
    public static void MapDeleteMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/membership/{id:int}", async 
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
                
            await cache.EvictByTagAsync(CacheTags.Memberships, ct);    
                
            return Results.Ok(new { Message = "Membership successfully removed!" });
        })
        .RequireAuthorization("Admin")
        .WithTags("Memberships");
    }
}