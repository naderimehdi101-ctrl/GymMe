namespace GymMe.Features.Members.CreateMember;

public sealed class CreateMemberValidator : AbstractValidator<CreateMemberRequest>
{
    public CreateMemberValidator()
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