using System;

namespace GymManagementApi.DTOs.Responses;

public sealed record GymPlanResponse
{
    public int ID { get; init; }
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
}
