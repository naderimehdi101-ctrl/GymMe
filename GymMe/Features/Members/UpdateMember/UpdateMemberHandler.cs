namespace GymMe.Features.Members.UpdateMember;

public sealed record UpdateMemberRequest(string Name, int Age, int GymPlanId);

public class UpdateMemberHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(int id, UpdateMemberRequest request)
    {
        var foundMember = await db.Members.FindAsync(id);
        if (foundMember is null)
            return Result.Fail($"No Member found with that ID: {id}!");

        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {request.GymPlanId}!");

        foundMember.Name = request.Name;
        foundMember.Age = request.Age;
        foundMember.GymPlanId = request.GymPlanId;

        await db.SaveChangesAsync();
        
        return Result.Ok();
    }
}