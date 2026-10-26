using System;

namespace GymManagementApi.DTOs.Responses;

public sealed class MemberResponse
{
    public int ID { get; init; }
    public string Name { get; init; } = "";
    public int Age { get; init; }
    public int GymPlanId { get; init; }
    public string GymPlanName { get; init; } = "";
    public decimal GymPlanPrice { get; init; }
    
}