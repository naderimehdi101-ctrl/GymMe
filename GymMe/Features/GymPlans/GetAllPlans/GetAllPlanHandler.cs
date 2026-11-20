namespace GymMe.Features.GymPlans.GetAllPlans;

public sealed record GymPlanResponse(int Id, string Name, decimal Price);

public sealed record PagedResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int? LastId { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
};

public class GetAllPlansHandler(AppDbContext db)
{
    public async Task<Result<PagedResponse<GymPlanResponse>>> HandleAsync(int? lastId, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageSize(lastId, pageSize);

        var allPlansCount = await db.Plans.CountAsync();

        var allPlans = await db.Plans
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Take(pageSize)
            .Select(x => new GymPlanResponse(x.Id, x.Name, x.Price))
            .AsNoTracking()
            .ToListAsync();

        return Result.Ok(new PagedResponse<GymPlanResponse>
        {
            Items = allPlans,
            LastId = lastId,
            PageSize = pageSize,
            TotalCount = allPlansCount
        });
    }
}