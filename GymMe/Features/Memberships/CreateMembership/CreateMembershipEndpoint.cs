


namespace GymMe.Features.Memberships.CreateMembership;

public static class CreateMembershipEndpoint
{
    public static void MapCreateMembershipEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/memberships", async 
        (
            CreateMembershipRequest request, 
            CreateMembershipHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request, ct);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagsAsync(ct, CacheTags.Memberships, CacheTags.Members);    
                
            return Results.Created($"/memberships/{result.Value.ID}", result.Value);
        })
        .RequireAuthorization("Admin")
        .AddEndpointFilter<ValidationFilter<CreateMembershipRequest>>()
        .WithTags("Memberships");
    }
}