using System;

namespace GymManagementApi.DTOs.Responses;

public sealed record GymPlanResponse(
    int ID, 
    string Name, 
    decimal Price
);
