using Microsoft.EntityFrameworkCore;

public class MemberService
{
    private readonly AppDbContext _db;

    public MemberService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Member?> CreateMember(CreateMemberDTO memberDto)
    {
        var idCounter = await _db.MemberIds.FirstOrDefaultAsync();

        if (idCounter is null)
        {
            idCounter = new MemberIDDTO
            {
                ID = 1,
                NextID = 1001
            };

            _db.MemberIds.Add(idCounter);
        }

        GymPlan? plan = await _db.GymPlans
			.FirstOrDefaultAsync(p => p.ID == memberDto.GymPlanId);

        if (plan is null)
            return null;

        var newMember = new Member
        {
            ID = idCounter.NextID,
            Name = memberDto.Name,
            Age = memberDto.Age,
            GymPlanId = memberDto.GymPlanId,
            GymPlan = plan
        };

        idCounter.NextID++;

        _db.GymMembers.Add(newMember);
        await _db.SaveChangesAsync();

        return newMember;
    }

    public async Task<Member?> GetMember(int id)
    {
        return await _db.GymMembers.FirstOrDefaultAsync(m => m.ID == id);
    }

    public async Task<Member?> UpdateMember(int id, CreateMemberDTO memberDto)
    {
        var found = await _db.GymMembers
			.FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            return null;

        GymPlan? plan = await _db.GymPlans
            .FirstOrDefaultAsync(p => p.ID == memberDto.GymPlanId);

        if (plan is null)
            return null;

        found.Name = memberDto.Name;
        found.Age = memberDto.Age;
        found.GymPlanId = memberDto.GymPlanId;
        found.GymPlan = plan;

        await _db.SaveChangesAsync();

        return found;
    }

    public async Task<Member?> DeleteMember(int id)
    {
        var found = await _db.GymMembers.FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            return null;

        _db.GymMembers.Remove(found);
        await _db.SaveChangesAsync();

        return found;
    }
}
