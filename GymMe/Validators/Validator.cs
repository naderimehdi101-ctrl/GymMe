using System;
using FluentValidation;

namespace GymManagementApi.Validators;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next )
    {
        var arg = context.Arguments.OfType<T>().FirstOrDefault();
        if(arg is null)
        {
            return Results.BadRequest(new { message = "The request body is missing or invalid!"});
        }
        
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if(validator is not null)
        {
            var validationResult = await validator.ValidateAsync(arg);
            
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary()); 
            }
        }
        
        return await next(context);
    }
}