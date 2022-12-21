using Domain.Accounts;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Accounts;

namespace Services.Accounts;

public class AccountService : IAccountService
{
    private readonly VicDBContext dbContext;
    private readonly DbSet<Account> accounts;

    public AccountService(VicDBContext dbContext)
    {
        this.dbContext = dbContext;
        accounts = this.dbContext.Accounts;
    }

    private IQueryable<Account> GetAccountById(long id)
    {
        return accounts
                .AsNoTracking()
                .Where(p => p.Id == id);
    }

    public async Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {

        AccountResponse.Create response = new();

        AccountDto.Mutate model = request.Account;

        Account account = new(model.Firstname, model.Lastname, model.Email, model.Role, model.Password, model.Department, model.Education, model.IsActive);
        var addedCustomer = accounts.Add(account);
        await dbContext.SaveChangesAsync();
        response.AccountId = addedCustomer.Entity.Id;
        return response;
    }

    public async Task DeleteAsync(AccountRequest.Delete request)
    {
        accounts.RemoveIf(account => account.Id == request.AccountId);
        await dbContext.SaveChangesAsync();
    }

    public async Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        AccountResponse.Edit response = new();
        var account = await GetAccountById(request.AccountId).SingleOrDefaultAsync();

        if (account is not null)
        {
            var model = request.Account;

            account.Firstname = model.Firstname;
            account.Lastname = model.Lastname;
            account.Email = model.Email;
            account.Role = model.Role;
            account.IsActive = model.IsActive;
            account.Education = model.Education;
            account.Department = model.Department;

            dbContext.Entry(account).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            response.AccountId = account.Id;
        }

        return response;
    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        AccountResponse.GetDetail response = new();
        Account? account = await GetAccountById(request.AccountId).SingleOrDefaultAsync();

        // Account with given Id does not exist. 
        if (account is null)
        {
            throw new EntityNotFoundException(nameof(Account), request.AccountId);
        }

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
        var query = accounts.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => $"{x.Firstname} {x.Lastname}".Contains(request.SearchTerm));
        }
        if (request.Roles is not null)
        {
            query = query.Where(x => request.Roles.Contains(x.Role.ToString()));
        }
        response.TotalAmount = await query.CountAsync();

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
