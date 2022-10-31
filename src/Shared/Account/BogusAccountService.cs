using Bogus;
using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Account;

public class BogusAccountService:IAccountService
{
    public readonly List<AccountDto.Details> accounts = new();

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
            .RuleFor(x => x.Role, f => f.PickRandom(new[] {"Master", "Admin", "Waarnemer"}))
            .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.Department, f => f.PickRandom(departments))
            .RuleFor(x => x.Education, f => f.PickRandom(educations));

        accounts = accountFaker.Generate(5);
    }

    public Task<AccountDto.Details> GetDetailAsync(long accountId)
    {
        return Task.FromResult(accounts.Single(x => x.Id == accountId));
    }

    public Task<IEnumerable<AccountDto.Index>> GetIndexAsync()
    {
        return Task.FromResult(accounts.Select(x => new AccountDto.Index
        {
            Id = x.Id,
            Name = x.Firstname + " " + x.Lastname,
            Email = x.Email
        }));
    }
}
