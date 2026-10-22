using Microsoft.EntityFrameworkCore;



public class MembershipService
{
    private readonly AppDbContext _db;

    public MembershipService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Membership?> CreateMembership(CreateMembershipDto dto)
    {
        var member = await _db.GymMembers
            .FirstOrDefaultAsync(m => m.ID == dto.MemberID);

        if (member is null)
            return null;

        var plan = await _db.GymPlans
            .FirstOrDefaultAsync(p => p.ID == dto.GymPlanID);

        if (plan is null)
            return null;

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

        return membership;
    }

}