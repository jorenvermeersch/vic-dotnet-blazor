using Microsoft.AspNetCore.Mvc;
using Shared.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Accounts;

[ApiController]
[Route("/api/accounts/")]
public class AccountController
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    /// <summary>
    /// Returns a list of accounts
    /// </summary>
    /// <returns></returns>
    [SwaggerOperation("Returns a list of accounts")]
    [HttpGet]
    public async Task<AccountResponse.GetIndex> GetIndexAsync([FromQuery] AccountRequest.GetIndex request)
    {
        return await accountService.GetIndexAsync(request);
    }


    /// <summary>
    /// Returns a specific user
    /// </summary>
    /// <returns></returns>
    [SwaggerOperation("Returns a specific account")]
    [HttpGet("{AccountId}")]
    public async Task<AccountResponse.GetDetail> GetDetailAsync([FromRoute] AccountRequest.GetDetail request)
    {
        return await accountService.GetDetailAsync(request);
    }
}
