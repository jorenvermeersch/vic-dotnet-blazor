using Shared.Accounts;

namespace Services.Accounts;

public class AccountService : IAccountService
{
    public AccountService()
    {

    }

    public Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AccountRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
