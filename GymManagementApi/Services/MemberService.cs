using Microsoft.EntityFrameworkCore;

public class MemberService
{
    private readonly AppDbContext _db;

    public MemberService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Member?> CreateMember(MemberDto memberDto)
    {
        var idCounter = await _db.MemberIds.FirstOrDefaultAsync();

        if (idCounter is null)
        {
            idCounter = new MemberIdDto
            {
                ID = 1,
                NextID = 1001
            };

            _db.MemberIds.Add(idCounter);
        }

        GymPlan? plan = await _db.Plans
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

        _db.Members.Add(newMember);
        await _db.SaveChangesAsync();

        return newMember;
    }

    public async Task<Member?> GetMember(int id)
    {
        return await _db.Members.Include(m => m.GymPlan).FirstOrDefaultAsync(m => m.ID == id);
    }

    public async Task<Member?> UpdateMember(int id, MemberDto memberDto)
    {
        var found = await _db.Members
			.FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            return null;

        GymPlan? plan = await _db.Plans
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
        var found = await _db.Members
            .Include(x => x.GymPlan)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (found is null)
            return null;

        _db.Members.Remove(found);
        await _db.SaveChangesAsync();

        return found;
    }
}
