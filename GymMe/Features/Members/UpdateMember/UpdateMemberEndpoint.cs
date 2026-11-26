namespace GymMe.Features.Members.UpdateMember;

public static class UpdateMemberEndpoint
{
    public static void MapUpdateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/members/{id:int}", async 
        (
            int id, 
            UpdateMemberRequest request, 
            UpdateMemberHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct 
            ) =>
        {
            var result = await handler.HandleAsync(id, request, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagsAsync(ct, CacheTags.Members, CacheTags.Memberships);
                
            return Results.Ok(new { Message = "Member successfully updated!" });
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<UpdateMemberRequest>>()
        .WithTags("Members");
    }
}