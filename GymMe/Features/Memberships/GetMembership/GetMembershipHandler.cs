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
    public async Task<Result<MembershipResponse>> HandleAsync(int id)
    {
        var membership = await db.Memberships
            .AsNoTracking()
            .Where(x => x.ID == id)
            .Select(x => new MembershipResponse(
                x.ID,
                x.MemberId,
                x.Member.Name,
                x.GymPlanId,
                x.GymPlan.Name,
                x.GymPlan.Price,
                x.StartDate,
                x.EndDate,
                x.IsActive
            ))
            .FirstOrDefaultAsync();

        if (membership is null)
            return Result.Fail<MembershipResponse>($"No Member found with that ID: {id}!");

        return Result.Ok(membership);
    }
}