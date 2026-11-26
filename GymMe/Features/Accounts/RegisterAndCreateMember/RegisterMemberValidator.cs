namespace GymMe.Features.Accounts.RegisterAndCreateMember;

public sealed class RegisterAndCreateMemberValidator : AbstractValidator<RegisterAndCreateMemberRequest>
{
    public RegisterAndCreateMemberValidator()
    {
        RuleFor(x => x.Email)
            .ValidEmail();

        RuleFor(x => x.Password)
            .ValidPassword();

        RuleFor(x => x.Name)
            .ValidPersonName();

        RuleFor(x => x.Age)
            .ValidMemberAge();

        RuleFor(x => x.GymPlanId)
            .ValidPlanId();
    }
}
