namespace GymMe.Features.Members.GetAllMembers;

public sealed record MemberResponse(int Id, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public class GetAllMembersHandler(AppDbContext db)
{
    public async Task<Result<CursorPagedResponse<MemberResponse>>> HandleAsync(int? lastId, int pageSize)
    {
        pageSize = PaginationHelper.ValidatePageSize(lastId, pageSize);

        var members = await db.Members
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Take(pageSize + 1)
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

        return Result.Ok(PaginationHelper.ToCursorResponse(
            members, 
            pageSize, 
            x => x.Id
        ));
    }
}