using Domain.Accounts;
using Domain.Constants;
using Fakers.Accounts;
using Shared.Accounts;

namespace Service.Accounts;

public class FakeAccountService : IAccountService {
    public readonly List<Account> accounts = new();

    public FakeAccountService() {
        AccountFaker accountsFaker = new();
        accounts = accountsFaker.UseSeed(1337).Generate(100);
    }

    public Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request) {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AccountRequest.Delete request) {
        throw new NotImplementedException();
    }

    public Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request) {
        throw new NotImplementedException();
    }

    public async Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request) {
        AccountResponse.GetDetail response = new();
        var query = accounts.AsQueryable();

        response.Account = query.Select(x => new AccountDto.Detail {
            Id = x.Id,
            Department = x.Department,
            Education = x.Education,
            Email = x.Email,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            IsActive = x.IsActive,
            Role = x.Role.ToString()
        }).SingleOrDefault(x => x.Id == request.AccountId) ?? new AccountDto.Detail();
        return response;
    }
       
    public async Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request) {
        AccountResponse.GetIndex response = new();
        var query = accounts.AsQueryable();

        response.TotalAmount = query.Count();

        query = query.Skip(request.Amount * request.Page);
        query = query.Take(request.Amount);

        query.OrderBy(x => x.Email);
        response.Accounts = query.Select(x => new AccountDto.Index {
            Id = x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            IsActive = x.IsActive,
            Lastname = x.Lastname,
            Role = x.Role.ToString()
        }).ToList();
        return response;
    }
}
