
namespace GymMe.Response.DTO;

public sealed record GymPlanResponse
{
    public int ID { get; init; }
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
}
