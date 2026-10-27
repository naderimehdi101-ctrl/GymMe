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
        var gymPlan = await _db.Plans.FirstOrDefaultAsync(p => p.ID == id);
        if(gymPlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");
        
        return new GymPlanResponse(
            ID: gymPlan.ID,
            Name: gymPlan.Name,
            Price: gymPlan.Price
        );
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

        return new GymPlanResponse(
            ID: newGymPlan.ID,
            Name: newGymPlan.Name,
            Price: newGymPlan.Price
        );
    }

    public async Task<bool> UpdatePlan(int id, GymPlanDto planDto)
    {
        var updatePlan = await _db.Plans.FirstOrDefaultAsync(p => p.ID == id);
        if(updatePlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        updatePlan.ID = id;
        updatePlan.Name = planDto.Name;
        updatePlan.Price = planDto.Price;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePlan(int id)
    {
        var deletePlan = await _db.Plans.FirstOrDefaultAsync(p => p.ID == id);
        if(deletePlan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {id}!");

        await _db.Plans.RemoveAsync(deletePlan);
        await _db.SaveChangesAsync();

        return true;
    }
}