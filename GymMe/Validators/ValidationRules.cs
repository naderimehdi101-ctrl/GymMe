
namespace GymMe.Validators;

public sealed class MemberDataValidator : AbstractValidator<MemberDto>
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

public sealed class GymPlanValidator : AbstractValidator<GymPlanDto>
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

public sealed class MembershipValidator : AbstractValidator<MembershipDto>
{
    public MembershipValidator()
    {
        RuleFor(x => x.DurationMonths)
                    .Must(duration => new[] { 1, 3, 6, 12 }.Contains(duration))
                    .WithMessage("Duration must be 1, 3, 6, or 12 months!");
    }
}

public sealed class RegistrationValidator : AbstractValidator<AccountDto>
{
    public RegistrationValidator()
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

public sealed class LoginValidator : AbstractValidator<LoginDto>
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