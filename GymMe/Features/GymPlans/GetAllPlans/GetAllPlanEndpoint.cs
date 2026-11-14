namespace GymMe.Features.GymPlans.GetAllPlans;

public static class GetAllPlansEndpoint
{
    public static void MapGetAllPlansEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/plans", async (GetAllPlansHandler handler, int page = 1, int pageSize = 10) =>
        {
            var response = await handler.HandleAsync(page, pageSize);
            if(response.IsFailed)
                return response.ToProblemDetails();
                
            return Results.Ok(response.Value);
        })
        .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(30)))
        .WithTags("GymPlans");
    }
}