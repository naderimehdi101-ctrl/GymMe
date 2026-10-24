using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

public static class MemberEndpoint
{
    public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/members").WithTags("Members");

        group.MapPost("/", CreateMemberHandler);
        group.MapPut("/{id:int}", UpdateMemberHandler);
        group.MapGet("/{id:int}", GetMemberHandler);
        group.MapDelete("/{id:int}", DeleteMemberHandler);
    }

    private static async Task<IResult> CreateMemberHandler(MemberDto memberDto, MemberService memberService, IValidator<MemberDto> validator)
    {
        var validationResults = await validator.ValidateAsync(memberDto);
        if (!validationResults.IsValid)
        {
            return Results.BadRequest(validationResults.ToDictionary());
        }
        var newMember = await memberService.CreateMember(memberDto);
        return Results.Created("Member created:", newMember);
    }

    private static async Task<IResult> GetMemberHandler(int id, MemberService memberService)
    {
        var getMember = await memberService.GetMember(id);
        return getMember is null
            ? Results.NotFound("No Member found with that ID!")
            : Results.Ok(getMember);
    }

    private static async Task<IResult> UpdateMemberHandler(int id, MemberDto memberDto, MemberService memberService, IValidator<MemberDto> validator)
    {
        var validationResults = await validator.ValidateAsync(memberDto);
        if (!validationResults.IsValid)
        {
            return Results.BadRequest(validationResults.ToDictionary());
        }

        var updateMember = await memberService.UpdateMember(id, memberDto);
        return updateMember is null
            ? Results.BadRequest("Member failed to update!")
            : Results.Ok("Member updated");
    }

    private static async Task<IResult> DeleteMemberHandler(int id, MemberService memberService)
    {
        var deleteMember = await memberService.DeleteMember(id);
        return deleteMember is null
            ? Results.NotFound("No Member with that ID!")
            : Results.Ok("Member successfully removed!\n" + deleteMember);
    }
}
