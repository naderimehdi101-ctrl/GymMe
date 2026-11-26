namespace GymMe.Features.Memberships.DeleteMembership;

public class DeleteMembershipHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id,
        CancellationToken ct = default)
    {
        var foundMembership = await db.Memberships.FindAsync(id, ct);
        if (foundMembership is null)
            return Result.Fail($"No Membership found with that ID: {id}!");

        if (foundMembership.IsActive)
            return Result.Fail("Cannot delete an active membership!");

        db.Memberships.Remove(foundMembership);
        await db.SaveChangesAsync(ct);
        
        return Result.Ok();
    }
}