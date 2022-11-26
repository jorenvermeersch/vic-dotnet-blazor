using Fakers.Accounts;
using Shared.Accounts;

namespace Service.Account;

public class FakeAccountService : IAccountService
{
    public readonly List<Domain.Administrators.Account> accounts = new();


    public FakeAccountService()
    {
        var accountsFaker = new AccountFaker();
        accounts = accountsFaker.UseSeed(1337).Generate(100);
    }

    public Task<AccountDto.Detail> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountDto.Detail> GetDetailAsync(long accountId)
    {
        AccountResponse.GetDetail response = new();
        var query = accounts.AsQueryable();

        response.Account = query.Select(x => new AccountDto.Detail
        {
            Id = (int)x.Id,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            Email = x.Email,
            Role = x.Role,
            IsActive = x.IsActive,
            PasswordHash = x.PasswordHash,
            Department = x.Department,
            Education = x.Education
        }).SingleOrDefault(x => x.Id == request.AccountId)!;

        return response;
    }

    //public async Task<AccountResponse.GetIndex> GetIndexAsync(int offset)
    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request)
    {
        AccountResponse.GetIndex response = new();
        var query = accounts.AsQueryable();

        response.TotalAmount = query.Count();

        response.Accounts = query.Select(x => new AccountDto.Index
        {
            Id = (int)x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            IsActive = x.IsActive,
            Role = x.Role
        }).ToList();

        return response;
    }

    //public Task<AccountDto.Details> Add(AccountDto.Create newAccount)
    //{
    //    Enum.TryParse(newAccount.Role, out Role role);

    //    Domain.Core.Account account = new()
    //    {
    //        Firstname = newAccount.Firstname,
    //        Lastname = newAccount.Lastname,
    //        Email = newAccount.Email,
    //        Role = role,
    //        IsActive = newAccount.IsActive,
    //        Department = newAccount.Department,
    //        Education = newAccount.Education,
    //        PasswordHash = newAccount.Password
    //    };
    //    accounts.Add(account);
    //    return Task.FromResult(account);
    //}

    //public Task<AccountDto.Details> GetDetailAsync(long accountId)
    //{
    //    return Task.FromResult(accounts.Single(x => x.Id == accountId));
    //}

    //public async Task<AccountResponse.GetIndex> GetIndexAsync(int offset)
    //{
    //    var query = dbContext.Accounts.AsQueryable();

    //    int totalAmount = await query.CountAsync();

    //    IEnumerable<AccountDto.Index> items = await query.Select(x => new AccountDto.Index
    //    {
    //        Id = (int)x.Id,
    //        Email= x.Email,
    //        Firstname= x.Firstname,
    //        Lastname= x.Lastname,
    //        IsActive= x.IsActive,
    //        Role = x.Role.ToString()
    //    }).ToListAsync();

    //    return items;


    //    //return Task.FromResult(accounts.Skip(offset).Take(20).Select(x => new AccountDto.Index
    //    //{
    //    //    Id = x.Id,
    //    //    Firstname = x.Firstname,
    //    //    Lastname = x.Lastname,
    //    //    Email = x.Email,
    //    //    Role = x.Role,
    //    //    IsActive = x.IsActive
    //    //}));
    //}
}
