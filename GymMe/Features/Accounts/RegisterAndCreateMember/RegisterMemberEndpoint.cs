using GymMe.Features.Accounts.RegisterAndCreateMember;

namespace GymMe.Features.Accounts.RegisterMember;

public static class RegisterAndCreateMemberEndpoint
{
    public static void MapRegisterAndCreateMemberEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/register-member-profile", async 
        (
            RegisterAndCreateMemberRequest request, 
            RegisterAndCreateMemberHandler handler,
            IOutputCacheStore cache,
            CancellationToken ct
            ) =>
        {
            var result = await handler.HandleAsync(request);
            if(result.IsFailed)
                return result.ToProblemDetails();
                
            await cache.EvictByTagAsync("members", ct);    
                
            return Results.Created($"/accounts/member/{result.Value.MemberId}", result.Value);
        })
        .AddEndpointFilter<ValidationFilter<RegisterAndCreateMemberRequest>>()
        .RequireAuthorization("Admin")
        .WithTags("Accounts");
    }
}