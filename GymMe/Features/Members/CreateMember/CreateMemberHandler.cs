namespace GymMe.Features.Members.CreateMember;

public sealed record CreateMemberRequest(string Name, int Age, int GymPlanId);
public sealed record MemberResponse(int ID, string Name, int Age, int GymPlanId, string GymPlanName, decimal GymPlanPrice);

public class CreateMemberHandler(AppDbContext db)
{
    public async Task<MemberResponse> HandleAsync(CreateMemberRequest request)
    {
        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            throw new KeyNotFoundException($"No Plan found with that ID: {request.GymPlanId}!");

        var newMember = new Member
        {
            Name = request.Name,
            Age = request.Age,
            GymPlanId = request.GymPlanId,
            GymPlan = plan
        };

        db.Members.Add(newMember);
        await db.SaveChangesAsync();

        return new MemberResponse(
            newMember.ID,
            newMember.Name,
            newMember.Age,
            newMember.GymPlanId,
            plan.Name,
            plan.Price
        );
    }
}