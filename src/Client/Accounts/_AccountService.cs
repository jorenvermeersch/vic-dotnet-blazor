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

    public Task<AccountDto.Detail> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<AccountResponse.GetDetail>($"api/accounts/{request.AccountId}");
        return response;
    }

    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<AccountResponse.GetIndex>("api/accounts");
        return response;
    }
}
