using Domain.Accounts;
using Fakers.Accounts;
using Services.FakeInitializer;
using Shared.Accounts;

namespace Service.Accounts;

public class FakeAccountService : IAccountService
{
    private static readonly List<Account> accounts = new();

    static FakeAccountService()
    {
        //var accountsFaker = new AccountFaker();
        //accounts = accountsFaker.UseSeed(1337).Generate(100);

        accounts = FakeInitializerService.FakeAccounts ?? new List<Account>();

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
