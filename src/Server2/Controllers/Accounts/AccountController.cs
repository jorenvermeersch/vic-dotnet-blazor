﻿using Microsoft.AspNetCore.Mvc;
using Shared.Accounts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Accounts;

[ApiController]
[Route("/api/accounts/")]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }


    [SwaggerOperation("Returns a list of accounts")]
    [HttpGet]
    public async Task<AccountResponse.GetIndex> GetIndexAsync([FromQuery] AccountRequest.GetIndex request)
    {
        return await accountService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific account")]
    [HttpGet("{AccountId}")]
    public async Task<AccountResponse.GetDetail> GetDetailAsync([FromRoute] AccountRequest.GetDetail request)
    {
        return await accountService.GetDetailAsync(request);
    }
}
