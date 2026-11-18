namespace GymMe.Features.Accounts.RegisterAndCreateMember;

public sealed class RegisterAndCreateMemberValidator : AbstractValidator<RegisterAndCreateMemberRequest>
{
    public RegisterAndCreateMemberValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty!")
            .EmailAddress().WithMessage("Invalid Email format!")
            .MaximumLength(100).WithMessage("Email maximum length: 100!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty!")
            .MinimumLength(8).WithMessage("Password minimum length: 8!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty!")
            .MaximumLength(100).WithMessage("Name maximum length: 100!");

        RuleFor(x => x.Age)
            .InclusiveBetween(14, 120).WithMessage("Age must be between 14 and 120!");

        RuleFor(x => x.GymPlanId)
            .GreaterThan(0).WithMessage("GymPlanId must be greater than 0!");
    }
}
