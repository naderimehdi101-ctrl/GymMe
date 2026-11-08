namespace GymMe.Features.GymPlans.GetPlan;

public sealed record GymPlanResponse(int ID, string Name, decimal Price);

public class GetPlanHandler(AppDbContext db)
{
    public async Task<GymPlanResponse> HandleAsync(int id)
    {
        var gymPlan = await db.Plans.FindAsync(id);
        if (gymPlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        return new GymPlanResponse(gymPlan.ID, gymPlan.Name, gymPlan.Price);
    }
}