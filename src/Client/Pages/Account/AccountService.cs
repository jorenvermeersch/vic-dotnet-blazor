using Client.Extensions;
using Shared.Account;
using System.Net.Http.Json;

namespace Client.Pages.Account
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient authenticatedClient;
        private const string endpoint = "api/accounts";
        public AccountService(HttpClient authenticatedClient)
        {
            this.authenticatedClient = authenticatedClient;
        }

        public Task<AccountDto.Details> Add(AccountDto.Create newAccount)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto.Details> GetDetailAsync(long AccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
        {
            var queryParameters = request.GetQueryString();
            var response = await authenticatedClient.GetFromJsonAsync<AccountResponse.GetIndex>($"{endpoint}/");
            Console.WriteLine(response);
            return response;
        }
    }
}
