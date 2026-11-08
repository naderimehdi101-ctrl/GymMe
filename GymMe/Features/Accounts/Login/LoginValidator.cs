namespace GymMe.Features.Accounts.Login;

public sealed class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty!")
            .EmailAddress()
            .WithMessage("Invalid Email format!")
            .MaximumLength(100)
            .WithMessage("Email maximum length: 100");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty!")
            .MinimumLength(8)
            .WithMessage("Password minimum length: 8");
    }
}