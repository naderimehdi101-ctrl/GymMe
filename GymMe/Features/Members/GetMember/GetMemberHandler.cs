namespace GymMe.Features.Members.GetMember;

public sealed record MemberResponse(int ID, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public class GetMemberHandler(AppDbContext db)
{
    public async Task<MemberResponse> HandleAsync(int id)
    {
        var member = await db.Members
            .Include(m => m.GymPlan)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (member is null)
            throw new KeyNotFoundException($"No Member found with that ID: {id}!");

        return new MemberResponse(
            member.ID,
            member.Name,
            member.Age,
            member.GymPlanId,
            member.GymPlan.Name,
            member.GymPlan.Price
        );
    }
}