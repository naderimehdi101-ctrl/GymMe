namespace GymMe.Features.Memberships.CreateMembership;

public sealed record CreateMembershipRequest(int MemberID, int GymPlanID, int DurationMonths);

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

public class CreateMembershipHandler(AppDbContext db)
{
    public async Task<MembershipResponse> HandleAsync(CreateMembershipRequest request)
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var member = await db.Members.FindAsync(request.MemberID);
        if (member is null)
            throw new KeyNotFoundException($"No Member found with that ID: {request.MemberID}!");

        var plan = await db.Plans.FindAsync(request.GymPlanID);
        if (plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {request.GymPlanID}!");

        var hasActiveMembership = await db.Memberships.AnyAsync(m => m.MemberId == member.ID && m.EndDate > startDate);
        if (hasActiveMembership)
            throw new InvalidOperationException("Member has already an active Membership!");

        var membership = new Membership
        {
            MemberId = request.MemberID,
            Member = member,
            GymPlanId = request.GymPlanID,
            GymPlan = plan,
            StartDate = startDate,
            EndDate = startDate.AddMonths(request.DurationMonths),
            DurationMonths = request.DurationMonths
        };

        db.Memberships.Add(membership);
        await db.SaveChangesAsync();

        return new MembershipResponse(
            membership.ID,
            membership.MemberId,
            member.Name,
            membership.GymPlanId,
            plan.Name,
            plan.Price,
            membership.StartDate,
            membership.EndDate,
            membership.IsActive
        );
    }
}