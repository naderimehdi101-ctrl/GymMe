using GymManagementApi.DTOs.Responses;
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

    public async Task<GymPlanResponse> GetPlan(int id)
    {
        var gymPlan = await _db.Plans.FindAsync(id);
        if(gymPlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");
        
        return ToResponse(gymPlan);
    }

    public async Task<GymPlanResponse> CreatePlan(GymPlanDto planDto)
    {
        var newGymPlan = new GymPlan
        {
            Name = planDto.Name,
            Price = planDto.Price
        };
        
        _db.Plans.Add(newGymPlan);
        await _db.SaveChangesAsync();

        return ToResponse(newGymPlan);
    }

    public async Task UpdatePlan(int id, GymPlanDto planDto)
    {
        var updatePlan = await _db.Plans.FindAsync(id);
        if(updatePlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        updatePlan.ID = id;
        updatePlan.Name = planDto.Name;
        updatePlan.Price = planDto.Price;

        await _db.SaveChangesAsync();
    }

    public async Task DeletePlan(int id)
    {
        var deletePlan = await _db.Plans.FindAsync(id);
        if(deletePlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        _db.Plans.Remove(deletePlan);
        await _db.SaveChangesAsync();
    }
    
    private GymPlanResponse ToResponse(GymPlan plan)
    {
        return new GymPlanResponse
        {
            ID = plan.ID,
            Name = plan.Name,
            Price = plan.Price
        };
    }
}