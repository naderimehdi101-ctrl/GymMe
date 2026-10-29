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
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);    
        
        var member = await _db.Members.FindAsync(dto.MemberID);
        if (member is null)
            throw new KeyNotFoundException($"No Member found with that ID: {dto.MemberID}!");

        var plan = await _db.Plans.FindAsync(dto.GymPlanID);
        if (plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {dto.GymPlanID}!");
            
        var hasActiveMembership = await _db.Memberships.AnyAsync(m => m.MemberId == member.ID && m.EndDate > startDate);
        if(hasActiveMembership)
            throw new InvalidOperationException("Member has already an active Membership!");
            
        var membership = new Membership
        {
            MemberId = dto.MemberID,
            Member = member,
            GymPlanId = dto.GymPlanID,
            GymPlan = plan,
            StartDate = startDate,
            EndDate = startDate.AddMonths(dto.DurationMonths),
            DurationMonths = dto.DurationMonths,
        };

        _db.Memberships.Add(membership);
        await _db.SaveChangesAsync();

        return ToResponse(membership);
    }
    
    public async Task<MembershipResponse> GetMembership(int id)
    {
        var getMembership = await _db.Memberships
            .Include(x => x.Member)
            .Include(x => x.GymPlan)
            .FirstOrDefaultAsync(x => x.ID == id);
        if(getMembership is null)
            throw new KeyNotFoundException($"No Membership found with that ID: {id}");
            
        return ToResponse(getMembership);
    }
    
    private MembershipResponse ToResponse(Membership membership)
    {
        return new MembershipResponse
        {
            ID = membership.ID,
            MemberID = membership.MemberId,
            MemberName = membership.Member.Name,
            GymPlanID = membership.GymPlanId,
            GymPlanName = membership.GymPlan.Name,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate,
            IsActive = membership.IsActive
        };    
    }
}