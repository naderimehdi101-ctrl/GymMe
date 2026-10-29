using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using GymManagementApi.Validators;



public static class MembershipEndpoints
{
    public static void MapMembershipEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/memberships").WithTags("Memberships");

        group.MapPost("/", CreateMembershipHandler).AddEndpointFilter<ValidationFilter<MembershipDto>>();
        group.MapGet("/{id:int}", GetMembershipHandler);
    }

    private static async Task<IResult> CreateMembershipHandler(MembershipDto membershipDto, MembershipService membershipService)
    {
        var newMembership = await membershipService.CreateMembership(membershipDto);
        return Results.Created($"/memberships/{newMembership.ID}", newMembership);
    }
    
    private static async Task<IResult> GetMembershipHandler(int id, MembershipService membershipService)
    {
        var getMembership = await membershipService.GetMembership(id);
        return Results.Ok(getMembership);
    }
}
