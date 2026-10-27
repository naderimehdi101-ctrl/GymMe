using System;
using FluentValidation;
using GymManagementApi.Validators;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace GymManagementApi.Validators;

public static class MemberEndpoint
{
    public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/members").WithTags("Members");

        group.MapPost("/", CreateMemberHandler).AddEndpointFilter<ValidationFilter<MemberDto>>();
        group.MapPut("/{id:int}", UpdateMemberHandler).AddEndpointFilter<ValidationFilter<MemberDto>>();
        group.MapGet("/{id:int}", GetMemberHandler);
        group.MapDelete("/{id:int}", DeleteMemberHandler);
        group.MapGet("/allMembers", GetAllMembersHandler);
    }

    private static async Task<IResult> CreateMemberHandler(MemberDto memberDto, MemberService memberService)
    {
        var response = await memberService.CreateMember(memberDto);
        return Results.Created($"Member created: {response.ID} ", response);
    }

    private static async Task<IResult> GetMemberHandler(int id, MemberService memberService)
    {
        var response = await memberService.GetMember(id);
        return Results.Ok(response);
    }

    private static async Task<IResult> UpdateMemberHandler(int id, MemberDto memberDto, MemberService memberService)
    {
        await memberService.UpdateMember(id, memberDto);
        return Results.Ok(new { Message = "Member successfully updated!" });
    }

    private static async Task<IResult> DeleteMemberHandler(int id, MemberService memberService)
    {
        await memberService.DeleteMember(id);
        return Results.Ok(new { Message = "Member successfully removed!" });
    }

    private static async Task<IResult> GetAllMembersHandler(MemberService memberService)
    {
        var getAllMembers = await memberService.GetAllMembers();
        return Results.Ok(getAllMembers);
    }
}
