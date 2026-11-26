namespace GymMe.Features.Members.CreateMember;

public sealed class CreateMemberValidator : AbstractValidator<CreateMemberRequest>
{
    public CreateMemberValidator()
    {
        RuleFor(x => x.Name)
            .ValidPersonName();

        RuleFor(x => x.Age)
            .ValidMemberAge();
    }
}