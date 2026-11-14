namespace GymMe.Features.GymPlans.GetPlan;

public sealed record GymPlanResponse(int ID, string Name, decimal Price);

public class GetPlanHandler(AppDbContext db)
{
    public async Task<Result<GymPlanResponse>> HandleAsync(int id)
    {
        var gymPlan = await db.Plans
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.ID == id);
        
        if (gymPlan is null)
            return Result.Fail<GymPlanResponse>($"No Plan found with that ID: {id}!");

        return Result.Ok(new GymPlanResponse(gymPlan.ID, gymPlan.Name, gymPlan.Price));
    }
}