﻿using Domain.Accounts;
using Domain.Constants;
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

    public async Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request) {
        AccountResponse.Create response = new();
        var query = accounts.AsQueryable();

        AccountDto.Mutate model = request.Account;

        Account acc = new Account() {
            Id = accounts.Max(x => x.Id) + 1,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            Role = Enum.Parse<Role>(model.Role, true),
            PasswordHash = model.Password,
            Department = model.Department,
            Education = model.Education,
            IsActive = model.IsActive
        };
        accounts.Add(acc);
        response.AccountId = acc.Id;
        return response;
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
            Role = x.Role
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
            Role = x.Role
        }).ToList();
        return response;
    }
}
