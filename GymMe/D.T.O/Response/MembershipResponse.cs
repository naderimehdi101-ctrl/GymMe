using System;

namespace GymManagementApi.DTOs.Responses;

public sealed record MembershipResponse
{
    public int ID { get; init; }
    public int MemberID { get; init; }
    public string MemberName { get; init; } = "";
    public int GymPlanID { get; init; }
    public string GymPlanName { get; init; } = "";
    public decimal PricePaid { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public bool IsActive { get; init; }
};
