using GymManagementApi.DTOs.Responses;
using GymMe;
using Microsoft.EntityFrameworkCore;

public class GymPlanService
{
    private readonly AppDbContext _db;

    public GymPlanService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<GymPlanResponse>> GetAllPlans(int page, int pageSize)
    {
        if(page < 1 || pageSize < 1)
            throw new ArgumentException("Page & Pagesize must both be atleast 1!");
        if(pageSize > 50)
            pageSize = 50;
            
        var allPlansCount = await _db.Plans.CountAsync();
        
        var allPlans = await _db.Plans
        .OrderBy(x => x.ID)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(x => new GymPlanResponse
        {
            ID = x.ID,
            Name = x.Name,
            Price = x.Price
        })
        .ToListAsync();
        
        return new PagedResponse<GymPlanResponse>
        {
            Items = allPlans,
            Page = page,
            PageSize = pageSize,
            TotalCount = allPlansCount
        };
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