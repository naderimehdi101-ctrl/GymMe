using System;
using FluentValidation;
using FluentValidation.Results;
using GymManagementApi.Validators;



public static class UserRegisterEndpoint
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Authentication");
        
        group.MapPost("/register", RegisterHandler).AddEndpointFilter<ValidationFilter<RegisterDto>>();
    }
    public static async Task<IResult> RegisterHandler(RegisterDto dto, AuthService authService)
    {
        var register = await authService.Register(dto);
        return Results.Created($"/auth/register", register);
    }
}
