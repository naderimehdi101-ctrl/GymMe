namespace GymMe.Features.Memberships.UpdateMembership;

public sealed record UpdateMembershipRequest(
    int GymPlanId,
    DateOnly StartDate,
    int DurationMonths
);

public class UpdateMembershipHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id,
        UpdateMembershipRequest request,
        CancellationToken ct = default)
    {
        var membership = await db.Memberships.FindAsync([id], ct);
        if (membership is null)
            return Result.Fail($"No Membership found with that ID: {id}!");

        var plan = await db.Plans.FindAsync([request.GymPlanId], ct);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {request.GymPlanId}!");

        var newEndDate = request.StartDate.AddMonths(request.DurationMonths);

        var hasOverlappingMembership = await db.Memberships.AnyAsync(
            m => m.MemberId == membership.MemberId
                 && m.Id != membership.Id
                 && m.StartDate <= newEndDate
                 && m.EndDate >= request.StartDate,
            ct);

        if (hasOverlappingMembership)
            return Result.Fail("Member already has another active membership currently!");

        membership.GymPlanId = request.GymPlanId;
        membership.StartDate = request.StartDate;
        membership.DurationMonths = request.DurationMonths;
        membership.EndDate = newEndDate;

        await db.SaveChangesAsync(ct);

        return Result.Ok();
    }
}