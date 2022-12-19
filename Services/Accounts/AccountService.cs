using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Accounts;
using Shared.Customers;

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

    private IQueryable<Account> GetAccountById(long id) => _accounts
                .AsNoTracking()
                .Where(p => p.Id == id);

    public async Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {

        AccountResponse.Create response = new();

        AccountDto.Mutate model = request.Account;

        Account account = new(model.Firstname, model.Lastname, model.Email, model.Role, model.Password, model.Department, model.Education, model.IsActive);
       var addedCustomer =  _accounts.Add(account);
        await _dbContext.SaveChangesAsync();
        response.AccountId = addedCustomer.Entity.Id;
        return response;
    }

    public async Task DeleteAsync(AccountRequest.Delete request)
    {
        _accounts.RemoveIf(account => account.Id == request.AccountId);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        AccountResponse.Edit response = new();
        var account = await GetAccountById(request.AccountId).SingleOrDefaultAsync();

        if (account is not null)
        {
            var model = request.Account;

            account.Firstname= model.Firstname;
            account.Lastname = model.Lastname; 
            account.Email= model.Email;
            account.Role= model.Role;
            account.IsActive= model.IsActive;
            account.Education= model.Education;
            account.Department= model.Department;

            _dbContext.Entry(account).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            response.AccountId = account.Id;
        }

        return response;
    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        AccountResponse.GetDetail response = new();
        Account account = await GetAccountById(request.AccountId).SingleOrDefaultAsync();

        response.Account = new AccountDto.Detail
        {
            Id = account.Id,
            Department = account.Department,
            Education = account.Education,
            Email = account.Email,
            Firstname = account.Firstname,
            Lastname = account.Lastname,
            IsActive = account.IsActive,
            Role = account.Role
        };

        return response;
    }

    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        
        AccountResponse.GetIndex response = new();
        var query = _accounts.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Firstname.Contains(request.SearchTerm)
                                  || x.Lastname.Contains(request.SearchTerm));
        }
        if (request.Roles is not null)
        {
            query = query.Where(x => request.Roles.Contains(x.Role.ToString()));
        }
        response.TotalAmount = query.Count();

        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        response.Accounts = await query.Select(x => new AccountDto.Index
        {
            Id = x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            IsActive = x.IsActive,
            Lastname = x.Lastname,
            Role = x.Role
        }).ToListAsync();

        return response;
    }
}
