namespace GymMe.Features.GymPlans.UpdatePlan;

public sealed class UpdatePlanValidator : AbstractValidator<UpdatePlanRequest>
{
    public UpdatePlanValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty!")
            .Length(3, 50)
            .WithMessage("Name length must be between 3 - 50 letters!");

        RuleFor(x => x.Price)
            .InclusiveBetween(1, 1000)
            .WithMessage("Price must be between 1 - 1000€!");
    }
}