namespace GymMe.Features.GymPlans.DeletePlan;

public class DeletePlanHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id,
        CancellationToken ct)
        {
            var foundPlan = await db.Plans.FindAsync([id], ct);
            if(foundPlan is null)
                return Result.Fail($"No Plan found with that ID: {id}");
                
            var hasMember = await db.Members.AnyAsync(x => x.GymPlanId == foundPlan.Id, ct);
            if(hasMember)
                return Result.Fail("Plan has active members");
                
            var hasMembership = await db.Memberships.AnyAsync(x => x.GymPlanId == foundPlan.Id, ct);
            if(hasMembership)
                return Result.Fail("Plan has active memberships");
                
            db.Plans.Remove(foundPlan);
            await db.SaveChangesAsync(ct);
            
            return Result.Ok();        
        }
}