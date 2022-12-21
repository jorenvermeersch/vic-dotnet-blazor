using Domain.Accounts;
using Domain.Constants;
using Domain.Exceptions;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Accounts;
using Shared.VirtualMachines;

namespace Services.Accounts;

public class AccountService : IAccountService
{
    private readonly VicDbContext dbContext;
    private readonly DbSet<Account> accounts;

    public AccountService(VicDbContext dbContext)
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
        throw new NotImplementedException();
    }

    public async Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {
        throw new NotImplementedException();
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

        // Fetch virtual machines of account.
        List<VirtualMachine>? machines = await dbContext.VirtualMachines.Where(machine => machine.Account.Id == request.AccountId).ToListAsync();

        List<VirtualMachineDto.Index>? machineIndexes = null;
        if (machines is not null)
        {
            machineIndexes = machines.Select(machine => new VirtualMachineDto.Index()
            {
                Id = machine.Id,
                Fqdn = machine.Fqdn,
                Status = machine.Status,
            }).ToList();
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
            Role = account.Role,
            VirtualMachines = machineIndexes,
        };

        return response;
    }

    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        AccountResponse.GetIndex response = new();
        var query = accounts.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(x => x.Firstname.Contains(searchTerm) || x.Lastname.Contains(searchTerm));
        }
        if (request.Roles is not null)
        {
            var roles = request.Roles.Select(role => Enum.Parse(typeof(Role), role)).ToList();
            query = query.Where(x => roles.Contains(x.Role));
        }
        response.TotalAmount = await query.CountAsync();

        query = query.OrderByDescending(x => x.CreatedAt);
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
