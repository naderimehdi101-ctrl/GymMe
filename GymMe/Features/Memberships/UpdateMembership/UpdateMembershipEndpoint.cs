namespace GymMe.Features.Memberships.UpdateMembership;

public static class UpdateMembershipEndpoint
{
    public static void MapUpdateMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/memberships/{id:int}", async (
            int id,
            UpdateMembershipRequest request,
            UpdateMembershipHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, request, ct);
            if (result.IsFailed)
                return result.ToProblemDetails();

            await cache.EvictByTagsAsync(ct, CacheTags.Memberships, CacheTags.Members);

            return Results.Ok(new { Message = "Membership successfully updated!" });
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<UpdateMembershipRequest>>()
        .WithTags("Memberships");
    }
}