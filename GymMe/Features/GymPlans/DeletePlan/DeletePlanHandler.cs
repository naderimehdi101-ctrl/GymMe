namespace GymMe.Features.GymPlans.DeletePlan;

public class DeletePlanHandler(AppDbContext db)
{
    public async Task HandleAsync(int id)
    {
        var deletePlan = await db.Plans.FindAsync(id);
        if (deletePlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        var hasMember = await db.Members.AnyAsync(x => x.GymPlanId == deletePlan.ID);
        if (hasMember)
            throw new InvalidOperationException("The Plan is still used by Members");

        var hasMembership = await db.Memberships.AnyAsync(x => x.GymPlanId == deletePlan.ID);
        if (hasMembership)
            throw new InvalidOperationException("The Plan is still used by Memberships");

        db.Plans.Remove(deletePlan);
        await db.SaveChangesAsync();
    }
}