namespace GymMe.Features.Members.GetAllMembers;

public sealed record MemberResponse(int Id, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public class GetAllMembersHandler(AppDbContext db)
{
    public async Task<Result<CursorPagedResponse<MemberResponse>>> HandleAsync(
        int? lastId, 
        int pageSize, 
        CancellationToken ct = default)
    {
        var pagedMembers = await db.Members
        .AsNoTracking()
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Select(x => new MemberResponse(
                x.Id,
                x.Name,
                x.Age,
                x.GymPlanId,
                x.GymPlan.Name,
                x.GymPlan.Price
            ))
            .ToCursorPageAsync(lastId, pageSize, x => x.Id, ct);
            
            return Result.Ok(pagedMembers);
    }
}