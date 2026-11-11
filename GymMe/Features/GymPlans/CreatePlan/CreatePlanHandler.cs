namespace GymMe.Features.GymPlans.CreatePlan;

public sealed record CreatePlanRequest(string Name, decimal Price);
public sealed record GymPlanResponse(int ID, string Name, decimal Price);

public class CreatePlanHandler(AppDbContext db)
{
    public async Task<Result<GymPlanResponse>> HandleAsync(CreatePlanRequest request)
    {
        var newGymPlan = new GymPlan
        {
            Name = request.Name,
            Price = request.Price
        };

        db.Plans.Add(newGymPlan);
        await db.SaveChangesAsync();

        return Result.Ok(new GymPlanResponse(newGymPlan.ID, newGymPlan.Name, newGymPlan.Price));
    }
}