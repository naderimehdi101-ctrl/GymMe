namespace GymMe.Features.Memberships.GetMembership;

public sealed record MembershipResponse(
    int ID,
    int MemberID,
    string MemberName,
    int GymPlanID,
    string GymPlanName,
    decimal PricePaid,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsActive
);

public class GetMembershipHandler(AppDbContext db)
{
    public async Task<MembershipResponse> HandleAsync(int id)
    {
        var membership = await db.Memberships
            .Include(x => x.Member)
            .Include(x => x.GymPlan)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (membership is null)
            throw new KeyNotFoundException($"No Membership found with that ID: {id}!");

        return new MembershipResponse(
            membership.ID,
            membership.MemberId,
            membership.Member.Name,
            membership.GymPlanId,
            membership.GymPlan.Name,
            membership.GymPlan.Price,
            membership.StartDate,
            membership.EndDate,
            membership.IsActive
        );
    }
}