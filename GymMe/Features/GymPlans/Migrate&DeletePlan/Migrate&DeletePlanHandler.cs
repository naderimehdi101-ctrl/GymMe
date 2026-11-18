
namespace GymMe.Features.GymPlans.MigrateAndDeletePlan;

public class MigrateAndDeletePlanHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(int oldPlanId, int targetPlanId)
    {
        if(oldPlanId == targetPlanId)
            return Result.Fail($"Source and target plan cannot be the same!");
            
        var oldPlan = await db.Plans.FindAsync(oldPlanId);
        if(oldPlan is null)
            return Result.Fail($"No Plan found with that ID: {oldPlanId}!");
            
        var targetPlan = await db.Plans.FindAsync(targetPlanId);
        if(targetPlan is null)
            return Result.Fail($"No Plan found with that ID: {targetPlanId}!");
            
        await using var transaction = await db.Database.BeginTransactionAsync();
        
        try
        {
            await db.Members
            .Where(x => x.GymPlanId == oldPlanId)
            .ExecuteUpdateAsync(setters =>
             setters.SetProperty(x => x.GymPlanId, targetPlanId));
            
            await db.Memberships
            .Where(x => x.GymPlanId == oldPlanId)
            .ExecuteUpdateAsync(setters => 
            setters.SetProperty(x => x.GymPlanId, targetPlanId));
            
            db.Plans.Remove(oldPlan);
            await db.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
            return Result.Ok();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;    
        }      
    }
}