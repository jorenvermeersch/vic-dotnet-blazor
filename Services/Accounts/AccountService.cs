using Shared.Account;

namespace Services.Accounts;

public class AccountService : IAccountService
{

    public AccountService()
    {

    }

    public Task<long> CreateAsync(AccountDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long accountId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(long accountId, AccountDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto.Detail> GetDetailAsync(long accountId)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
