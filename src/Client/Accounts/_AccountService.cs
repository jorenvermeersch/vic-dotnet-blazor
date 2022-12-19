using Shared.Accounts;
using System.Net.Http.Json;

namespace Client.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient client;
    private const string endpoint = "api/accounts";
    public AccountService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        //var queryParameters = request.GetQueryString();
        //objectextentions ondersteunt geen list
        string queryParameters = string.Format("page={0}&amount={1}", request.Page, request.Amount);
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            queryParameters += "&searchterm=" + request.SearchTerm;
        }
        if (request.Roles != null)
        {
            queryParameters += "&roles=" + string.Join("&roles=", request.Roles);
        }

        var response = await client.GetFromJsonAsync<AccountResponse.GetIndex>($"api/accounts?{queryParameters}");
        return response!;
    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        var response = await client.GetFromJsonAsync<AccountResponse.GetDetail>($"api/accounts/{request.AccountId}");
        return response!;
    }

    public async Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {
        var response = await client.PostAsJsonAsync(endpoint, request);
        var content = await response.Content.ReadFromJsonAsync<AccountResponse.Create>();
        return content!;
    }

    public async Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        var response = await client.PutAsJsonAsync(endpoint, request);
        var content = await response.Content.ReadFromJsonAsync<AccountResponse.Edit>();
        return content!;
    }

    public Task DeleteAsync(AccountRequest.Delete request)
    {
        throw new NotImplementedException();
    }
}
