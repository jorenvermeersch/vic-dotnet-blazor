using Bogus;
using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Constants;

namespace Shared.Account;

public class BogusAccountService : IAccountService
{
    public readonly List<AccountDto.Details> accounts = new();
    private readonly VicDBContext dbContext;

    public BogusAccountService()
    {
        var accountID = 0;

        var departments = new[] { "DIT", "DBT" };
        var educations = new[] { "", "Toegepaste Informatica", "Elektro-mechanica" };

        var accountFaker = new Faker<AccountDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => accountID++)
            .RuleFor(x => x.Firstname, f => f.Name.FirstName())
            .RuleFor(x => x.Lastname, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Role, f => f.PickRandom(Enum.GetValues(typeof(Role)).Cast<Role>().ToArray()))
            .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.Department, f => f.PickRandom(departments))
            .RuleFor(x => x.Education, f => f.PickRandom(educations));

        accounts = accountFaker.Generate(10);
    }
    public BogusAccountService(VicDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<AccountDto.Details> Add(AccountDto.Create newAccount)
    {
        AccountDto.Details account = new()
        {
            Id = accounts.Count+1,
            Firstname = newAccount.Firstname,
            Lastname = newAccount.Lastname,
            Email = newAccount.Email,
            Role = TranslateEnums.TranslateRole(newAccount.Role),
            IsActive = newAccount.IsActive,
            Department = newAccount.Department,
            Education = newAccount.Education,
            PasswordHash = newAccount.Password
        };
        accounts.Add(account);
        return Task.FromResult(account);
    }

    public Task<AccountDto.Details> GetDetailAsync(long accountId)
    {
        return Task.FromResult(accounts.Single(x => x.Id == accountId));
    }

    public async Task<IEnumerable<AccountDto.Index>> GetIndexAsync(int offset)
    {
        var query = dbContext.Accounts.AsQueryable();

        int totalAmount = await query.CountAsync();

        IEnumerable<AccountDto.Index> items = await query.Select(x => new AccountDto.Index
        {
            Id = (int)x.Id,
            Email = x.Email,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            IsActive = x.IsActive,
            Role = x.Role
        }).ToListAsync();

        return items;


        //return Task.FromResult(accounts.Skip(offset).Take(20).Select(x => new AccountDto.Index
        //{
        //    Id = x.Id,
        //    Firstname = x.Firstname,
        //    Lastname = x.Lastname,
        //    Email = x.Email,
        //    Role = x.Role,
        //    IsActive = x.IsActive
        //}));
    }
}
