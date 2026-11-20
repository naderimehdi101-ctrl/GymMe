namespace GymMe.Features.Members.GetAllMembers;

public sealed record MemberResponse(int ID, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public sealed record PagedResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int? LastId { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
}

public class GetAllMembersHandler(AppDbContext db)
{
    public async Task<Result<PagedResponse<MemberResponse>>> HandleAsync(int? lastId, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageSize(lastId, pageSize);

        var totalCount = await db.Members.CountAsync();

        var members = await db.Members
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Take(pageSize)
            .Select(x => new MemberResponse(
                x.Id,
                x.Name,
                x.Age,
                x.GymPlanId,
                x.GymPlan.Name,
                x.GymPlan.Price
            ))
            .AsNoTracking()
            .ToListAsync();

        return Result.Ok(new PagedResponse<MemberResponse>
        {
            Items = members,
            LastId = lastId,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}