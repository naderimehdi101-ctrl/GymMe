namespace GymMe.Features.MyProfile.GetMyProfile;

public sealed record MyProfileResponse(
    int Id,
    string Name,
    int Age,
    int GymPlanId,
    string GymPlanName,
    decimal GymPlanPrice,
    Guid Version
);

public class GetMyProfileHandler(AppDbContext db)
{
    public async Task<Result<MyProfileResponse>> HandleAsync(
        int myId, 
        CancellationToken ct = default)
        {
            var member = await db.Members
                .AsNoTracking()
                .Where(x => x.Id == myId)
                .Select(x => new MyProfileResponse(
                    x.Id,
                    x.Name,
                    x.Age,
                    x.GymPlanId,
                    x.GymPlan.Name,
                    x.GymPlan.Price,
                    x.Version
                ))
                .FirstOrDefaultAsync(ct);
                
            if(member is null)
                return Result.Fail<MyProfileResponse>("No Member profile is linked to this account!");
                
            return Result.Ok();    
        }
}