using GymManagementApi.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

public class MemberService
{
    private readonly AppDbContext _db;

    public MemberService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<MemberResponse> CreateMember(MemberDto memberDto)
    {
        var plan = await GetPlanOrThrow(memberDto.GymPlanId);
        
        var newMember = new Member
        {
            Name = memberDto.Name,
            Age = memberDto.Age,
            GymPlanId = memberDto.GymPlanId,
            GymPlan = plan
        };

        _db.Members.Add(newMember);
        await _db.SaveChangesAsync();

        return ToResponse(newMember);
    }

    public async Task UpdateMember(int id, MemberDto memberDto)
    {
        var found = await _db.Members.FindAsync(id);
        if (found is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}!");
        
        await GetPlanOrThrow(memberDto.GymPlanId);
        
        found.Name = memberDto.Name;
        found.Age = memberDto.Age;
        found.GymPlanId = memberDto.GymPlanId;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteMember(int id)
    {
        var found = await _db.Members.FindAsync(id);

        if (found is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}");
        
        var hasMembership = await _db.Memberships.AnyAsync(x => x.MemberId == found.ID  && x.EndDate > DateOnly.FromDateTime(DateTime.UtcNow));
        if(hasMembership)
            throw new InvalidOperationException("Member has a membership!");

        _db.Members.Remove(found);
        await _db.SaveChangesAsync();
    }
    
    public async Task<MemberResponse> GetMember(int id)
    {
        var member = await _db.Members
            .Include(m => m.GymPlan)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (member is null)
            throw new KeyNotFoundException($"No Member Found!");

        return ToResponse(member);
    }

    public async Task<List<MemberResponse>> GetAllMembers()
    {
        var allMembers = await _db.Members
        .Select(x => new MemberResponse
        {
            ID = x.ID,
            Name = x.Name,
            Age = x.Age,
            GymPlanId = x.GymPlanId,
            GymPlanName = x.GymPlan.Name,
            GymPlanPrice = x.GymPlan.Price,
        })
        .ToListAsync();

        return allMembers;
    }
    
    private MemberResponse ToResponse(Member member)
    {
        return new MemberResponse
        {
            ID = member.ID,
            Name = member.Name,
            Age = member.Age,
            GymPlanId = member.GymPlanId,
            GymPlanName = member.GymPlan.Name,
            GymPlanPrice = member.GymPlan.Price
        };
    }
    
    private async Task<GymPlan> GetPlanOrThrow(int planId)
    {
        var plan = await _db.Plans.FindAsync(planId);
        if(plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {planId}!");
            
        return plan;    
    }
}
