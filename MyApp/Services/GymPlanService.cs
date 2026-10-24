using Microsoft.EntityFrameworkCore;

public class GymPlanService
{
    private readonly AppDbContext _db;

    public GymPlanService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<GymPlan>> GetAllPlans()
    {
        return await _db.Plans.ToListAsync();
    }

    public async Task<GymPlan?> GetPlan(int id)
    {
        return await _db.Plans
            .FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<GymPlan> CreatePlan(GymPlanDto planDto)
    {
        var newGymPlan = new GymPlan
        {
            Name = planDto.Name,
            Price = planDto.Price
        };

        _db.Plans.Add(newGymPlan);
        await _db.SaveChangesAsync();

        return newGymPlan;
    }
}