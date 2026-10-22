using FluentValidation;

public sealed class MemberDataValidator : AbstractValidator<CreateMemberDTO>
{
    public MemberDataValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Name must not be empty!")
                    .Length(3, 50)
                    .WithMessage("Name length must be between 3 - 50 letters!");

        RuleFor(x => x.Age)
                    .InclusiveBetween(14, 100)
                    .WithMessage("Age must be between 14 - 100!");
    }
}

public sealed class GymPlanValidator : AbstractValidator<GymPlanDTO>
{
    public GymPlanValidator()
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

public sealed class MembershipService : AbstractValidator<CreateMembershipDto>
{
    public MembershipService()
    {
        RuleFor(x => x.DurationMonths)
        .Must(duration => new[] { 1, 3, 6, 12 }.Contains(duration))
        .WithMessage("Duration must be 1, 3, 6, or 12 months!");
    }
}