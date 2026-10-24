using Microsoft.EntityFrameworkCore;
using FluentValidation;

public static class MembershipEndpoints
{
    public static void MapMembershipEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Group routes under "/memberships" and tag them for Swagger
        var group = app.MapGroup("/memberships").WithTags("Memberships");

        // 2. Map the POST endpoint to our private handler method
        group.MapPost("/", CreateMembershipHandler);
    }

    private static async Task<IResult> CreateMembershipHandler(MembershipDto membershipDto, IValidator<MembershipDto> validator, MembershipService membershipService)
    {
        // Validate incoming DTO data using FluentValidation
        var validationResult = await validator.ValidateAsync(membershipDto);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.ToDictionary());
        }

        // Pass to the service to attempt membership generation
        var newMembership = await membershipService.CreateMembership(membershipDto);
        if (newMembership is null)
        {
            return Results.BadRequest("No Membership created!");
        }

        // Standard 201 Created package pointing to the new resource location
        return Results.Created($"/memberships/{newMembership.ID}", newMembership);
    }
}
