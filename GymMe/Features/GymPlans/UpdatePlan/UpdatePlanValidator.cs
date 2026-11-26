namespace GymMe.Features.GymPlans.UpdatePlan;

public sealed class UpdatePlanValidator : AbstractValidator<UpdatePlanRequest>
{
    public UpdatePlanValidator()
    {
        RuleFor(x => x.Name)
            .ValidPlanName();

        RuleFor(x => x.Price)
            .ValidPlanPrice();
    }
}