using Microsoft.AspNetCore.Mvc;
using Shared.Account;
using Swashbuckle.AspNetCore.Annotations;
using Domain.Core;

namespace Server.Controllers.Accounts;

[ApiController]
[Route("/api/accounts")]
public class AccountController : Controller
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }


    [SwaggerOperation("Returns a list of accounts")]
    [HttpGet]
    public async Task<AccountResponse.GetIndex> GetIndex()
    {
        AccountRequest.GetIndex request = new() { Offset = 50 };
        AccountResponse.GetIndex accountResponse = await accountService.GetIndexAsync(request);
        return accountResponse;
    }

}
