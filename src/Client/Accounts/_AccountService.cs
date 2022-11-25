using Client.Extensions;
using Shared.Account;
using System.Net.Http.Json;

namespace Client.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient authenticatedClient;
    private const string endpoint = "api/accounts";
    public AccountService(HttpClient authenticatedClient)
    {
        this.authenticatedClient = authenticatedClient;
    }

    public Task<AccountDto.Details> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountResult.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<AccountResult.GetDetail>($"api/accounts/{request.AccountId}");
        return response;
    }

    public async Task<AccountResult.Index> GetIndexAsync(AccountRequest.Index request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<AccountResult.Index>("api/accounts");
        return response;
    }
}
