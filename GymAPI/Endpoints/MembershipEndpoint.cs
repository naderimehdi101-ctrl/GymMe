using System;
using FluentValidation;
using GymManagementApi.Validators;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;



namespace GymManagementApi.Validators;

public static class MembershipEndpoints
{
    public static void MapMembershipEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/memberships").WithTags("Memberships");

        group.MapPost("/", CreateMembershipHandler).AddEndpointFilter<ValidationFilter<MembershipDto>>();
    }

    private static async Task<IResult> CreateMembershipHandler(MembershipDto membershipDto, IValidator<MembershipDto> validator, MembershipService membershipService)
    {
        var newMembership = await membershipService.CreateMembership(membershipDto);
        return Results.Created($"/memberships/{newMembership.ID}", newMembership);
    }
}
