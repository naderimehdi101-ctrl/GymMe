namespace GymMe.Features.GymPlans.DeletePlan;

public class DeletePlanHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(int id)
    {
        var deletePlan = await db.Plans.FindAsync(id);
        if (deletePlan is null)
            return Result.Fail($"No Plan found with that ID: {id}!");

        var hasMember = await db.Members.AnyAsync(x => x.GymPlanId == deletePlan.ID);
        if (hasMember)
            return Result.Fail("The Plan is still used by Members");

        var hasMembership = await db.Memberships.AnyAsync(x => x.GymPlanId == deletePlan.ID);
        if (hasMembership)
            return Result.Fail("The Plan is still used by Memberships");

        db.Plans.Remove(deletePlan);
        await db.SaveChangesAsync();
        
        return Result.Ok();
    }
}