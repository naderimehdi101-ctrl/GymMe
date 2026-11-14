namespace GymMe.Features.GymPlans.GetPlan;

public sealed record GymPlanResponse(int ID, string Name, decimal Price);

public class GetPlanHandler(AppDbContext db)
{
    public async Task<Result<GymPlanResponse>> HandleAsync(int id)
    {
        var gymPlan = await db.Plans
        .AsNoTracking()
        .Where(x => x.ID == id)
        .Select(x => new GymPlanResponse(
            x.ID,
            x.Name,
            x.Price
        ))
        .FirstOrDefaultAsync();
        
        if (gymPlan is null)
            return Result.Fail<GymPlanResponse>($"No Plan found with that ID: {id}!");

        return Result.Ok(gymPlan);
    }
}