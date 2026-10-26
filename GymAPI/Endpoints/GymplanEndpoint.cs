using System;
using FluentValidation;
using GymManagementApi.Validators;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;



namespace GymManagementApi.Validators;

public static class GymPlanEndpoints
{
    public static void MapGymPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/gymplans").WithTags("Gym Plans");

        group.MapPost("/", CreatePlanHandler).AddEndpointFilter<ValidationFilter<GymPlanDto>>();
        group.MapGet("/", GetAllPlansHandler);
        group.MapGet("/{id:int}", GetPlanHandler);
    }

    private static async Task<IResult> CreatePlanHandler(GymPlanDto planDto, GymPlanService gymPlanService)
    {
        var response = await gymPlanService.CreatePlan(planDto);
  
        return Results.Created($"/gymplans/{response.ID}", response);
    }

    private static async Task<IResult> GetAllPlansHandler(GymPlanService gymPlanService)
    {
        var plans = await gymPlanService.GetAllPlans();
        return Results.Ok(plans);
    }

    private static async Task<IResult> GetPlanHandler(int id, GymPlanService gymPlanService)
    {
        var plan = await gymPlanService.GetPlan(id);
        return Results.Ok(plan);
    }
}
