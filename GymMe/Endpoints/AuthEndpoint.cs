using System;
using FluentValidation;
using FluentValidation.Results;
using GymManagementApi.Validators;

namespace GymMe;

public static class UserRegisterEndpoint
{
    public static void MapUserEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/user").WithTags("Authentication");
        
        group.MapPost("/register", CreateUserHandler).AddEndpointFilter<ValidationFilter<RegisterDto>>();
    }
    public static async Task<IResult> CreateAuthHandler(RegisterDto dto, AuthService authService)
    {
        var register = await authService.Register(dto);
        return Results.Created($"/user", register);
    }
}
