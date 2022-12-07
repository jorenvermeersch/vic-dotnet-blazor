using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shared.Accounts;
using Shared.Hosts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Accounts;

[ApiController]
[Route("/api/accounts/")]
public class AccountController : ControllerBase {
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService) {
        this.accountService = accountService;
    }


    [SwaggerOperation("Returns a list of accounts")]
    [HttpGet]
    public async Task<AccountResponse.GetIndex> GetIndexAsync([FromQuery] AccountRequest.GetIndex request) {
        return await accountService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific account")]
    [HttpGet("{AccountId}")]
    public async Task<AccountResponse.GetDetail> GetDetailAsync([FromRoute] AccountRequest.GetDetail request) {
        return await accountService.GetDetailAsync(request);
    }

    [SwaggerOperation("Create a new account")]
    [HttpPost]
    public async Task<AccountResponse.Create> CreateAsync([FromBody] AccountRequest.Create request) {
        return await accountService.CreateAsync(request);
    }

    [SwaggerOperation("Edits accounts.")]
    [HttpPut]
    public async Task<IActionResult> EditAsync( AccountRequest.Edit request)
    {
        AccountResponse.Edit response = await accountService.EditAsync(request);
        return Accepted(nameof(EditAsync), response);
    }

    [SwaggerOperation("Deletes accounts.")]
    [HttpDelete("{accountId}")]
    public async Task<IActionResult> Delete(long accountId)
    {
        await accountService.DeleteAsync(new AccountRequest.Delete { AccountId = accountId });
        return NoContent();
    }
}