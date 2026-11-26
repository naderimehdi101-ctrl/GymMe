namespace GymMe.Features.Accounts.RegisterTrainer;

public sealed class RegisterTrainerValidator : AbstractValidator<RegisterTrainerRequest>
{
    public RegisterTrainerValidator()
    {
        RuleFor(x => x.Email)
            .ValidEmail();

        RuleFor(x => x.Password)
            .ValidPassword();
    }
}