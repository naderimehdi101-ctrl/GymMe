namespace GymMe;

public sealed class ValidationFilter<TRequest>(IValidator<TRequest> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestArgument = context.Arguments.FirstOrDefault(argument => argument is TRequest)
            ?? throw new InvalidOperationException($"No request argument of type {typeof(TRequest).Name} was provided.");

        var validationResult = await validator.ValidateAsync(
            (TRequest)requestArgument,
            context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}
