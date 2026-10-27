using GymManagementApi.DTOs.Responses;
using Microsoft.EntityFrameworkCore;


public class MembershipService
{
    private readonly AppDbContext _db;

    public MembershipService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<MembershipResponse> CreateMembership(MembershipDto dto)
    {
        var member = await _db.Members
            .FirstOrDefaultAsync(m => m.ID == dto.MemberID);

        if (member is null)
            throw new KeyNotFoundException($"No Member found with that ID: {dto.MemberID}!");

        var plan = await _db.Plans
            .FirstOrDefaultAsync(p => p.ID == dto.GymPlanID);

        if (plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {dto.GymPlanID}!");

        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var membership = new Membership
        {
            MemberId = dto.MemberID,
            Member = member,
            GymPlanId = dto.GymPlanID,
            GymPlan = plan,
            StartDate = startDate,
            EndDate = startDate.AddMonths(dto.DurationMonths),
            DurationMonths = dto.DurationMonths
        };

        _db.Memberships.Add(membership);
        await _db.SaveChangesAsync();

        return new MembershipResponse(
            ID = membership.ID,
            MemberID = membership.MemberId,
            MemberName = membership.Member.Name,
            GymPlanID = membership.GymPlanId,
            GymPlanName = membership.GymPlan.Name,
            PricePaid = membership.GymPlan.Price,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate,
            IsActive = membership.IsActive
        );
    }
}