namespace GymMe.Features.Members.GetAllMembers;

public sealed record MemberResponse(int ID, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public sealed record PagedResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
}

public class GetAllMembersHandler(AppDbContext db)
{
    public async Task<Result<PagedResponse<MemberResponse>>> HandleAsync(int page, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageAndPagesize(page, pageSize);

        var totalCount = await db.Members.CountAsync();

        var members = await db.Members
            .OrderBy(x => x.ID)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new MemberResponse(
                x.ID,
                x.Name,
                x.Age,
                x.GymPlanId,
                x.GymPlan.Name,
                x.GymPlan.Price
            ))
            .ToListAsync();

        return Result.Ok(new PagedResponse<MemberResponse>
        {
            Items = members,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}