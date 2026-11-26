namespace GymMe.Features.Memberships.UpdateMembership;

public sealed class UpdateMembershipValidator : AbstractValidator<UpdateMembershipRequest>
{
    public UpdateMembershipValidator()
    {
        RuleFor(x => x.GymPlanId)
            .ValidPlanId();

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate must not be empty!");

        RuleFor(x => x.DurationMonths)
            .Must(duration => new[] { 1, 3, 6, 12 }.Contains(duration))
            .WithMessage("Duration must be 1, 3, 6, or 12 months!");
    }
}