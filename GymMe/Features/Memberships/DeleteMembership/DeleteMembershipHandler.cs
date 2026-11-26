namespace GymMe.Features.Memberships.DeleteMembership;

public class DeleteMembershipHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id,
        CancellationToken ct)
    {
        var foundMembership = await db.Memberships.FindAsync(id);
        if (foundMembership is null)
            return Result.Fail($"No Membership found with that ID: {id}!");

        var hasMembership = await db.Memberships.AnyAsync(x => x.MemberId == foundMembership.Id && x.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow));
        if (hasMembership)
            return Result.Fail("Member has an active membership!");

        db.Memberships.Remove(foundMembership);
        await db.SaveChangesAsync();
        
        return Result.Ok();
    }
}