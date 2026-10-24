using FluentValidation;

public static class GymPlanEndpoints
{
    public static void MapGymPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/gymplans").WithTags("Gym Plans");

        group.MapPost("/", CreatePlanHandler);
        group.MapGet("/", GetAllPlansHandler);
        group.MapGet("/{id:int}", GetPlanHandler);
    }

    private static async Task<IResult> CreatePlanHandler(GymPlanDto planDto, GymPlanService gymPlanService, IValidator<GymPlanDto> validator)
    {
        var validationResults = await validator.ValidateAsync(planDto);
        if (!validationResults.IsValid)
        {
            return Results.BadRequest(validationResults.ToDictionary());
        }

        var newPlan = await gymPlanService.CreatePlan(planDto);

        return Results.Created($"/gymplans/{newPlan.ID}", newPlan);
    }

    private static async Task<IResult> GetAllPlansHandler(GymPlanService gymPlanService)
    {
        var plans = await gymPlanService.GetAllPlans();
        return plans is null
            ? Results.NotFound("No gymplan found!")
            : Results.Ok(plans);
    }

    private static async Task<IResult> GetPlanHandler(int id, GymPlanService gymPlanService)
    {
        var plan = await gymPlanService.GetPlan(id);
        
        return plan is null
            ? Results.NotFound("No plan found!")
            : Results.Ok(plan);
    }
}
