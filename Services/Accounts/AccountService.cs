using Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Accounts;

namespace Services.Accounts;

public class AccountService : IAccountService
{
    private readonly VicDBContext _dbContext;
    private readonly DbSet<Account> _accounts;

    public AccountService(VicDBContext dbContext)
    {
        _dbContext = dbContext;
        _accounts = _dbContext.Accounts;
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
