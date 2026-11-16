namespace GymMe.Features.Memberships.CreateMembership;

public sealed record CreateMembershipRequest(int MemberId, int GymPlanId, int DurationMonths);

public sealed record MembershipResponse(
    int ID,
    int MemberId,
    string MemberName,
    int GymPlanId,
    string GymPlanName,
    decimal PricePaid,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsActive
);

public class CreateMembershipHandler(AppDbContext db)
{
    public async Task<Result<MembershipResponse>> HandleAsync(CreateMembershipRequest request)
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var member = await db.Members.FindAsync(request.MemberId);
        if (member is null)
            return Result.Fail<MembershipResponse>($"No Member found with that ID: {request.MemberId}!");

        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            return Result.Fail<MembershipResponse>($"No Plan found with that ID: {request.GymPlanId}!");

        var hasActiveMembership = await db.Memberships.AnyAsync(m => m.MemberId == member.Id && m.EndDate > startDate);
        if (hasActiveMembership)
            return Result.Fail<MembershipResponse>("Member has already an active Membership!");

        member.Version = Guid.NewGuid();

        var membership = new Membership
        {
            MemberId = request.MemberId,
            Member = member,
            GymPlanId = request.GymPlanId,
            GymPlan = plan,
            StartDate = startDate,
            EndDate = startDate.AddMonths(request.DurationMonths),
            DurationMonths = request.DurationMonths
        };

        db.Memberships.Add(membership);
        await db.SaveChangesAsync();

        return Result.Ok(new MembershipResponse(
            membership.Id,
            membership.MemberId,
            member.Name,
            membership.GymPlanId,
            plan.Name,
            plan.Price,
            membership.StartDate,
            membership.EndDate,
            membership.IsActive
        ));
    }
}