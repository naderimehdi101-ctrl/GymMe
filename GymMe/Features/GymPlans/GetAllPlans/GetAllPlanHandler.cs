namespace GymMe.Features.GymPlans.GetAllPlans;

public sealed record GymPlanResponse(int ID, string Name, decimal Price);

public sealed class PagedResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
}

public class GetAllPlansHandler(AppDbContext db)
{
    public async Task<PagedResponse<GymPlanResponse>> HandleAsync(int page, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageAndPagesize(page, pageSize);

        var allPlansCount = await db.Plans.CountAsync();

        var allPlans = await db.Plans
            .OrderBy(x => x.ID)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GymPlanResponse(x.ID, x.Name, x.Price))
            .ToListAsync();

        return new PagedResponse<GymPlanResponse>
        {
            Items = allPlans,
            Page = page,
            PageSize = pageSize,
            TotalCount = allPlansCount
        };
    }
}