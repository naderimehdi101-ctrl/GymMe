namespace GymMe.Features.Members.UpdateMember;

public sealed record UpdateMemberRequest(string Name, int Age, int GymPlanId, Guid Version);

public class UpdateMemberHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id, 
        UpdateMemberRequest request,
        CancellationToken ct)
    {
        var foundMember = await db.Members.FindAsync(id);
        if (foundMember is null)
            return Result.Fail($"No Member found with that ID: {id}!");
            
        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {request.GymPlanId}!");
            
        db.Entry(foundMember).Property(x => x.Version).OriginalValue = request.Version;    

        foundMember.Name = request.Name;
        foundMember.Age = request.Age;
        foundMember.GymPlanId = request.GymPlanId;
        foundMember.Version = Guid.NewGuid();

        await db.SaveChangesAsync(ct);
        
        return Result.Ok();
    }
}