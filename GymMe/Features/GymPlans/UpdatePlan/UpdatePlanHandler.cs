namespace GymMe.Features.GymPlans.UpdatePlan;

public sealed record UpdatePlanRequest(string Name, decimal Price, Guid Version);

public class UpdatePlanHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(int id, UpdatePlanRequest request)
    {
        var plan = await db.Plans.FindAsync(id);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {id}!");
            
        db.Entry(plan)
          .Property(x => x.Version)
          .OriginalValue = request.Version;    

        plan.Name = request.Name;
        plan.Price = request.Price;
        plan.Version = Guid.NewGuid();

        await db.SaveChangesAsync();
        return Result.Ok();
    }
}