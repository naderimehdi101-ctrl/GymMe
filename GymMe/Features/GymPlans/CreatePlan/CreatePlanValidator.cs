namespace GymMe.Features.GymPlans.CreatePlan;

public sealed class CreatePlanValidator : AbstractValidator<CreatePlanRequest>
{
    public CreatePlanValidator()
    {
        RuleFor(x => x.Name)
            .ValidPlanName();

        RuleFor(x => x.Price)
            .ValidPlanPrice();
    }        
}