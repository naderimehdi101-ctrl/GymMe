namespace GymMe.Features.GymPlans.GetAllPlans;

public sealed record GymPlanResponse(int Id, string Name, decimal Price);

public class GetAllPlansHandler(AppDbContext db)
{
    public async Task<Result<CursorPagedResponse<GymPlanResponse>>> HandleAsync(int? lastId, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageSize(lastId, pageSize);

        var plans = await db.Plans
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Take(pageSize + 1)
            .Select(x => new GymPlanResponse(x.Id, x.Name, x.Price))
            .AsNoTracking()
            .ToListAsync();

        return Result.Ok(PaginationHelper.ToCursorResponse(
            plans,
            pageSize, 
            x => x.Id
        ));
    }
}