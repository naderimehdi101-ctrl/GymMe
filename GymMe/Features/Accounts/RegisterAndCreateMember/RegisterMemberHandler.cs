using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace GymMe.Features.Accounts.RegisterAndCreateMember;

public sealed record RegisterAndCreateMemberRequest(
    string Email,
    string Password,
    string Name,
    int Age,
    int GymPlanId
    );

public sealed record RegisterAndCreateMemberResponse(
    int UserId,
    int MemberId,
    string Email,
    string Name,
    int Age,
    int GymPlanId,
    string GymPlanName
    );

public class RegisterAndCreateMemberHandler(AppDbContext db, IPasswordHasher<User> hasher)
{
    public async Task<Result<RegisterAndCreateMemberResponse>> HandleAsync(RegisterAndCreateMemberRequest request)
    {
        var email = request.Email.ToLower().Trim();

        var emailExists = await db.Users.AnyAsync(x => x.Email == email);
        if (emailExists)
            return Result.Fail<RegisterAndCreateMemberResponse>("The Email already exists!");

        var plan = await db.Plans.FindAsync(request.GymPlanId);
        if (plan is null)
            return Result.Fail<RegisterAndCreateMemberResponse>($"No Plan found with that ID: {request.GymPlanId}");

        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var user = new User
            {
                Email = email,
                Role = Role.Member
            };

            user.PasswordHash = hasher.HashPassword(user, request.Password);
            
            db.Users.Add(user);
            await db.SaveChangesAsync();

            var member = new Member
            {
                Id = user.Id,
                Name = request.Name,
                Age = request.Age,
                GymPlanId = request.GymPlanId,
                UserId = user.Id
            };
            
            db.Members.Add(member);
            await db.SaveChangesAsync();
            
            await transaction.CommitAsync();

            return Result.Ok(new RegisterAndCreateMemberResponse(
            user.Id,
            member.Id,
            email,
            member.Name,
            member.Age,
            member.GymPlanId,
            plan.Name));
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}