namespace GymMe.Features.Memberships.CreateMembership;

public sealed class CreateMembershipValidator : AbstractValidator<CreateMembershipRequest>
{
    public CreateMembershipValidator()
    {
        RuleFor(x => x.DurationMonths)
            .Must(duration => new[] { 1, 3, 6, 12 }.Contains(duration))
            .WithMessage("Duration must be 1, 3, 6, or 12 months!");
    }
}
