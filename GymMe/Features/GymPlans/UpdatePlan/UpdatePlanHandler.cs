namespace GymMe.Features.GymPlans.UpdatePlan;

public sealed record UpdatePlanRequest(string Name, decimal Price);

public class UpdatePlanHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(int id, UpdatePlanRequest request)
    {
        var plan = await db.Plans.FindAsync(id);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {id}!");

        plan.Name = request.Name;
        plan.Price = request.Price;

        await db.SaveChangesAsync();
        
        return Result.Ok();
    }
}