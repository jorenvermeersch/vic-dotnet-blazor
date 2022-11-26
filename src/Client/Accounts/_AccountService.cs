using Shared.Accounts;

namespace Client.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient authenticatedClient;
    private const string endpoint = "api/accounts";
    public AccountService(HttpClient authenticatedClient)
    {
        this.authenticatedClient = authenticatedClient;
    }

    public Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AccountRequest.Delete request)
    {
        throw new NotImplementedException();
    }


    // TODO: Implement new methods are remove this old code. 


    //public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    //{
    //    var queryParameters = request.GetQueryString();
    //    var response = await authenticatedClient.GetFromJsonAsync<AccountResponse.GetDetail>($"api/accounts/{request.AccountId}");
    //    return response;
    //}

    //public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    //{
    //    var queryParameters = request.GetQueryString();
    //    var response = await authenticatedClient.GetFromJsonAsync<AccountResponse.GetIndex>("api/accounts");
    //    return response;
    //}
}
