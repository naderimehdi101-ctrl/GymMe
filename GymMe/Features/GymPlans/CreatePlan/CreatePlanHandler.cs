namespace GymMe.Features.GymPlans.CreatePlan;

public sealed record CreatePlanRequest(string Name, decimal Price);
public sealed record GymPlanResponse(int Id, string Name, decimal Price);

public class CreatePlanHandler(AppDbContext db)
{
    public async Task<Result<GymPlanResponse>> HandleAsync(
        CreatePlanRequest request,
        CancellationToken ct)
    {
        var newGymPlan = new GymPlan
        {
            Name = request.Name,
            Price = request.Price
        };

        db.Plans.Add(newGymPlan);
        await db.SaveChangesAsync(ct);

        return Result.Ok(new GymPlanResponse(newGymPlan.Id, newGymPlan.Name, newGymPlan.Price));
    }
}