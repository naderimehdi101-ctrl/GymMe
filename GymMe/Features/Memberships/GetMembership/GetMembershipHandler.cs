namespace GymMe.Features.Memberships.GetMembership;

public sealed record MembershipResponse(
    int Id,
    int MemberId,
    string MemberName,
    int GymPlanId,
    string GymPlanName,
    decimal PricePaid,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsActive
);

public class GetMembershipHandler(AppDbContext db)
{
    public async Task<Result<MembershipResponse>> HandleAsync(int id, CancellationToken ct = default)
    {
        var membership = await db.Memberships
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new MembershipResponse(
                x.Id,
                x.MemberId,
                x.Member.Name,
                x.GymPlanId,
                x.GymPlan.Name,
                x.GymPlan.Price,
                x.StartDate,
                x.EndDate,
                x.IsActive
            ))
            .FirstOrDefaultAsync(ct);

        if (membership is null)
            return Result.Fail<MembershipResponse>($"No Membership found with that ID: {id}!");

        return Result.Ok(membership);
    }
}