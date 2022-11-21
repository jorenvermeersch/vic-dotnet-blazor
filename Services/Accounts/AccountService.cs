
using Domain.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Shared.Account;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

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


    public Task<AccountDto.Details> Add(AccountDto.Create newAccount)
    {
        throw new NotImplementedException();
    }


    //public async Task<AccountResponse.GetIndex> GetIndexAsync(int offset)
    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        AccountResponse.GetIndex response = new();
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

    public Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }
}
