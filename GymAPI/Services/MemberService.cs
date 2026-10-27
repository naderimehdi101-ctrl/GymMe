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
        GymPlan? plan = await _db.Plans
            .FirstOrDefaultAsync(p => p.ID == memberDto.GymPlanId);

        if (plan is null)
            throw new KeyNotFoundException($"No Plan Found!");

        var newMember = new Member
        {
            Name = memberDto.Name,
            Age = memberDto.Age,
            GymPlanId = memberDto.GymPlanId,
            GymPlan = plan
        };

        _db.Members.Add(newMember);
        await _db.SaveChangesAsync();

        return new MemberResponse {
            ID = newMember.ID,
            Name = newMember.Name,
            Age = newMember.Age,
            GymPlanId = newMember.GymPlanId,
            GymPlanName = plan.Name,
            GymPlanPrice = plan.Price
        };
    }

    public async Task<MemberResponse> GetMember(int id)
    {
        var member = await _db.Members.Include(m => m.GymPlan).FirstOrDefaultAsync(m => m.ID == id);

        if (member is null)
            throw new KeyNotFoundException($"No Member Found!");

        return new MemberResponse {
            ID = member.ID,
            Name = member.Name,
            Age = member.Age,
            GymPlanId = member.GymPlanId,
            GymPlanName = member.GymPlan?.Name ?? "No plan assigned",
            GymPlanPrice = member.GymPlan?.Price ?? 0.00m
        };
    }

    public async Task<bool> UpdateMember(int id, MemberDto memberDto)
    {
        var found = await _db.Members
            .FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}!");

        GymPlan? plan = await _db.Plans
            .FirstOrDefaultAsync(p => p.ID == memberDto.GymPlanId);

        if (plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID!");

        found.Name = memberDto.Name;
        found.Age = memberDto.Age;
        found.GymPlanId = memberDto.GymPlanId;
        found.GymPlan = plan;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMember(int id)
    {
        var found = await _db.Members
            .Include(x => x.GymPlan)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}");

        _db.Members.Remove(found);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<List<MemberResponse>> GetAllMembers()
    {
        var allMembers = await _db.Members.Include(m => m.GymPlan).ToListAsync();
        if(allMembers is null || allMembers.Count == 0)
            throw new KeyNotFoundException($"No Members Found!");)

        return allMembers;
    }
        
    
}
