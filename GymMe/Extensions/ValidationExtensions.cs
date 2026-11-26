namespace GymMe.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("Email cannot be empty!")
            .EmailAddress().WithMessage("Invalid Email format!")
            .MaximumLength(100).WithMessage("Email maximum length: 100!");

    public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("Password cannot be empty!")
            .MinimumLength(8).WithMessage("Password minimum length: 8!");

    public static IRuleBuilderOptions<T, string> ValidPersonName<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length must be between 3 - 50 letters!");

    public static IRuleBuilderOptions<T, int> ValidMemberAge<T>(this IRuleBuilder<T, int> ruleBuilder)
        => ruleBuilder
            .InclusiveBetween(14, 100).WithMessage("Age must be between 14 - 100!");

    public static IRuleBuilderOptions<T, string> ValidPlanName<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length must be between 3 - 50 letters!");

    public static IRuleBuilderOptions<T, decimal> ValidPlanPrice<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        => ruleBuilder
            .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 - 1000€!");

    public static IRuleBuilderOptions<T, int> ValidPlanId<T>(this IRuleBuilder<T, int> ruleBuilder)
        => ruleBuilder
            .GreaterThan(0).WithMessage("GymPlanId must be greater than 0!");
}
