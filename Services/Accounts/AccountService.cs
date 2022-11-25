using Domain.Administrators;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Account;

namespace Services.Accounts;

public class AccountService : IAccountService
{

    //private readonly System.Data.Entity.Infrastructure.IDbContextFactory dbContext;
    public AccountService(VicDBContext dbContext)
    {
        this._dbContext = dbContext;
        _accounts = _dbContext.Accounts;
    }

    private readonly VicDBContext _dbContext;
    private readonly DbSet<Account> _accounts;


    public Task<AccountDto.Details> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }


    //public async Task<AccountResponse.GetIndex> GetIndexAsync(int offset)
    public async Task<AccountResult.Index> GetIndexAsync(AccountRequest.Index request)
    {
        AccountResult.Index response = new();
        var query = _accounts.AsQueryable().AsNoTracking();

        response.TotalAmount = query.Count();

        response.Accounts = await _dbContext.Accounts.Select(x => new AccountDto.Index
        {
            Id = (int)x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            IsActive = x.IsActive,
            Role = x.Role
        }).ToListAsync();

        return response;
    }

    public Task<AccountResult.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }
}
