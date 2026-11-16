namespace GymMe.Features.GymPlans.GetPlan;

public sealed record GymPlanResponse(int Id, string Name, decimal Price, Guid Version);

public class GetPlanHandler(AppDbContext db)
{
    public async Task<Result<GymPlanResponse>> HandleAsync(int id)
    {
        var gymPlan = await db.Plans
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new GymPlanResponse(
            x.Id,
            x.Name,
            x.Price,
            x.Version
        ))
        .FirstOrDefaultAsync();
        
        if (gymPlan is null)
            return Result.Fail<GymPlanResponse>($"No Plan found with that ID: {id}!");

        return Result.Ok(gymPlan);
    }
}