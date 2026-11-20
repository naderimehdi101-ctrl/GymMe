namespace GymMe.Features.GymPlans.GetAllPlans;

public sealed record GymPlanResponse(int Id, string Name, decimal Price);

public class GetAllPlansHandler(AppDbContext db)
{
    public async Task<Result<CursorPagedResponse<GymPlanResponse>>> HandleAsync(
        int? lastId, 
        int pageSize, 
        CancellationToken ct = default)
    {
        var pagedPlans = await db.Plans
            .AsNoTracking()
            .Where(x => lastId == null || x.Id > lastId)
            .OrderBy(x => x.Id)
            .Select(x => new GymPlanResponse(x.Id, x.Name, x.Price))
            .ToCursorPageAsync(lastId, pageSize, x => x.Id, ct);

        return Result.Ok(pagedPlans);
    }
}