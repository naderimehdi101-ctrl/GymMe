
namespace GymMe.Endpoint;

public static class AccountEndpoint
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/accounts").WithTags("Registration");
        
        group.MapPost("/member", MemberAccountHandler).AddEndpointFilter<ValidationFilter<AccountDto>>();
        group.MapPost("/trainer", TrainerAccountHandler).AddEndpointFilter<ValidationFilter<AccountDto>>();
        group.MapPost("/login", LoginAccountHandler);
    }
    public static async Task<IResult> MemberAccountHandler(AccountDto accountDto, AccountService accountService)
    {
        var register = await accountService.Register(accountDto);
        return Results.Created($"/accounts/member", register);
    }
    
    public static async Task<IResult> TrainerAccountHandler(AccountDto accountDto, AccountService accountService)
    {
        var register = await accountService.RegisterTrainer(accountDto);
        return Results.Created($"/accounts/trainer", register);
    } 
    
    public static async Task<IResult> LoginAccountHandler(LoginDto loginDto, AccountService accountService)
    {
        var login = await accountService.LoginAccount(loginDto);
        return Results.Ok(login);
    }
}
