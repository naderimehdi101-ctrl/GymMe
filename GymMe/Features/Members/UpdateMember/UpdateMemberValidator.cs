namespace GymMe.Features.Members.UpdateMember;

public sealed class UpdateMemberValidator : AbstractValidator<UpdateMemberRequest>
{
    public UpdateMemberValidator()
    {
        RuleFor(x => x.Name)
            .ValidPersonName();

        RuleFor(x => x.Age)
            .ValidMemberAge();
    }
}