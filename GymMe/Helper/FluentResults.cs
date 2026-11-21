
namespace GymMe.Helpers;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this ResultBase result)
    {
        var errors = result.Reasons.Select(r => r.Message).ToList();

        return Results.BadRequest(new { errors });
    }
}