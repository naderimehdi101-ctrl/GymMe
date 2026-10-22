using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=members.db"));

// Register application services with Dependency Injection (DI).
builder.Services.AddScoped<GymPlanService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<MembershipService>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Services for swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");

app.MapPost("/members", async (CreateMemberDTO memberDto, MemberService memberService, IValidator<CreateMemberDTO> validator) =>
{
    var validationResults = await validator.ValidateAsync(memberDto);
    if (!validationResults.IsValid)
    {
        return Results.BadRequest(validationResults.ToDictionary());
    }
    var newMember = await memberService.CreateMember(memberDto);

    return Results.Created("Member created:", newMember);
});

app.MapPost("/gymPlanMaker", async (GymPlanService gymPlanService, GymPlanDTO planDto, IValidator<GymPlanDTO> validator) =>
{
    var validationResults = await validator.ValidateAsync(planDto);
    if (!validationResults.IsValid)
    {
        return Results.BadRequest(validationResults.ToDictionary());
    }
    var newPlan = await gymPlanService.CreatePlan(planDto);

    return Results.Created($"/gymplan/{newPlan.ID}", newPlan);
});

app.MapPost("/memberships", async (CreateMembershipDto membershipDto, IValidator<CreateMembershipDto> validator, MembershipService membershipService) =>
{
    var validationResult = await validator.ValidateAsync(membershipDto);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.ToDictionary());
    }

    var newMembership = await membershipService.CreateMembership(membershipDto);
    if (newMembership is null)
    {
        return Results.BadRequest("No Membership created!");
    }
    return Results.Created($"/memberships/{newMembership.ID}", newMembership);
});

app.MapPut("/members/{id:int}", async (int id, CreateMemberDTO memberDto, MemberService memberService, IValidator<CreateMemberDTO> validator) =>
{
    var validationResults = await validator.ValidateAsync(memberDto);

    if (!validationResults.IsValid)
    {
        return Results.BadRequest(validationResults);
    }

    var updateMember = await memberService.UpdateMember(id, memberDto);
    if (updateMember is null)
    {
        return Results.BadRequest("Member failed to update!");
    }

    return Results.Ok("Member updated");
});

app.MapGet("/error", () =>
{
    return Results.Problem("Something went wrong");
});

app.MapDelete("/members/{id:int}", async (int id, MemberService memberService) =>
{
    var deleteMember = await memberService.DeleteMember(id);
    if (deleteMember is null)
    {
        return Results.NotFound("No Member with that ID!");
    }

    return Results.Ok(deleteMember);
});

app.MapGet("/gymPlans", async (GymPlanService gymPlanService) =>
{
    var plans = await gymPlanService.GetAllPlans();
    return Results.Ok(plans);
});

app.MapGet("/member/{id:int}", async (int id, MemberService memberService) =>
{
    var getMember = await memberService.GetMember(id);

    if (getMember is null)
    {
        return Results.NotFound("No ID match found!");
    }

    return Results.Ok(getMember);
});

app.MapGet("/gymPlan/{id:int}", async (int id, GymPlanService gymPlanService) =>
{
    var plan = await gymPlanService.GetPlan(id);
    if (plan is null)
    {
        return Results.NotFound("No Plan found!");
    }

    return Results.Ok(plan);
});
app.MapGet("/allMembers", (AppDbContext db) =>
{
    var members = db.GymMembers.Where(p => p.GymPlanId == 1);
    if (members is null)
    {
        return Results.BadRequest("No Member found!");
    }

    return Results.Ok(members);
});



app.Run();


