namespace GymMe.Features.Members.DeleteMember;

public class DeleteMemberHandler(AppDbContext db)
{
    public async Task HandleAsync(int id)
    {
        var foundMember = await db.Members.FindAsync(id);
        if (foundMember is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}");

        var hasMembership = await db.Memberships.AnyAsync(x => x.MemberId == foundMember.ID && x.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow));
        if (hasMembership)
            throw new InvalidOperationException("Member has an active membership!");

        db.Members.Remove(foundMember);
        await db.SaveChangesAsync();
    }
}