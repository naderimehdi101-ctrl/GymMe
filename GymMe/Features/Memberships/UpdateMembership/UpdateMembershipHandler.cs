namespace GymMe.Features.Memberships.UpdateMembership;

public sealed record UpdateMembershipRequest(
    string Name, 
    int Age, 
    int GymPlanId, 
    Guid Version);

public class UpdateMembershipHandler(AppDbContext db)
{
    public async Task<Result> HandleAsync(
        int id, 
        UpdateMembershipRequest request,
        CancellationToken ct)
    {
        var foundMembership = await db.Members.FindAsync(id);
        if (foundMembership is null)
            return Result.Fail($"No Member found with that ID: {id}!");
            
        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            return Result.Fail($"No Plan found with that ID: {request.GymPlanId}!");
            
        db.Entry(foundMembership).Property(x => x.Version).OriginalValue = request.Version;    

        foundMembership.Name = request.Name;
        foundMembership.Age = request.Age;
        foundMembership.GymPlanId = request.GymPlanId;
        foundMembership.Version = Guid.NewGuid();

        await db.SaveChangesAsync(ct);
        
        return Result.Ok();
    }
}