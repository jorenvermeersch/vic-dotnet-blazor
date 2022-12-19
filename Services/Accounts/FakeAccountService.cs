using Domain.Accounts;
using Services.FakeInitializer;
using Shared.Accounts;

namespace Service.Accounts;

public class FakeAccountService : IAccountService
{

    public static List<Account> Accounts { get; private set; } = new List<Account>();
    //private static readonly List<Account> accounts = new();

    static FakeAccountService()
    {
        Accounts = FakeInitializerService.FakeAccounts ?? new List<Account>();
    }

    public async Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request)
    {
        AccountResponse.Create response = new();
        var query = Accounts.AsQueryable();

        AccountDto.Mutate model = request.Account;

        Account acc = new(model.Firstname, model.Lastname, model.Email, model.Role, model.Password, model.Department, model.Education, model.IsActive)
        {
            Id = Accounts.Max(x => x.Id) + 1
        };
        Accounts.Add(acc);
        response.AccountId = acc.Id;
        return response;
    }

    public async Task DeleteAsync(AccountRequest.Delete request)
    {
        var account = Accounts.SingleOrDefault(x => x.Id == request.AccountId);
        if (account != null)
            Accounts.Remove(account);
    }

    public async Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request)
    {

        AccountResponse.Edit response = new();
        var account = Accounts.SingleOrDefault(x => x.Id == request.AccountId);

        if (account == null)
        {
            response.AccountId = -1;
            return response;
        }

        var model = request.Account;

        account.Firstname = model.Firstname;
        account.Lastname = model.Lastname;
        account.Email = model.Email;
        account.Role = model.Role;
        account.Department = model.Department;
        account.Education = model.Education;
        account.IsActive = model.IsActive;

        response.AccountId = account.Id;
        return response;

    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request)
    {
        AccountResponse.GetDetail response = new();
        var query = Accounts.AsQueryable();

        response.Account = query.Select(x => new AccountDto.Detail
        {
            Id = x.Id,
            Department = x.Department,
            Education = x.Education,
            Email = x.Email,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            IsActive = x.IsActive,
            Role = x.Role
        }).SingleOrDefault(x => x.Id == request.AccountId) ?? new AccountDto.Detail();
        return response;
    }

    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        AccountResponse.GetIndex response = new();
        var query = Accounts.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Firstname.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                                  || x.Lastname.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        if (request.Roles is not null)
        {
            query = query.Where(x => request.Roles.Contains(x.Role.ToString()));
        }
        response.TotalAmount = query.Count();

        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        query.OrderBy(x => x.Email);
        response.Accounts = query.Select(x => new AccountDto.Index
        {
            Id = x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            IsActive = x.IsActive,
            Lastname = x.Lastname,
            Role = x.Role
        }).ToList();
        return response;
    }
}
