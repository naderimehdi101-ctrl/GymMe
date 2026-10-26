using System;

namespace GymManagementApi.DTOs.Responses;

public sealed record MembershipResponse(
    int ID,
    int MemberID,
    string MemberName,
    int GymPlanID,
    string GymPlanName,
    decimal PricePaid,
    DateOnly StartDate,
    DateOnly EndDate
);
