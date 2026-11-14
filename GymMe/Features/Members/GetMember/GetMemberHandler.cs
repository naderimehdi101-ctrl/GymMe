namespace GymMe.Features.Members.GetMember;

public sealed record MemberResponse(int ID, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public class GetMemberHandler(AppDbContext db)
{
    public async Task<Result<MemberResponse>> HandleAsync(int id)
    {
        var member = await db.Members
            .AsNoTracking()
            .Where(m => m.ID == id)
            .Select(x => new MemberResponse(
            x.ID,
            x.Name,
            x.Age,
            x.GymPlanId,
            x.GymPlan.Name,
            x.GymPlan.Price
    ))
    .FirstOrDefaultAsync();

        if (member is null)
            return Result.Fail<MemberResponse>($"No Member found with that ID: {id}!");

        return Result.Ok(member);
    }
}