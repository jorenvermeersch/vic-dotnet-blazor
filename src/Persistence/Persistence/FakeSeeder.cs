using Domain.Core;
using Fakers.Accounts;

namespace Persistence;

public class FakeSeeder
{
    private readonly VicDBContext dbContext;

    public FakeSeeder(VicDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        SeedAccounts();
    }

    private void SeedAccounts()
    {
        //var accounts = new Faker<Account>("nl")
        //    .UseSeed(1337)
        //    .RuleFor(x => x.Id, _ => accountID++)
        //    .RuleFor(x => x.Firstname, f => f.Name.FirstName())
        //    .RuleFor(x => x.Lastname, f => f.Name.LastName())
        //    .RuleFor(x => x.Email, f => f.Internet.Email())
        //    .RuleFor(x => x.Role, f => f.PickRandom(new[] { "Master", "Admin", "Waarnemer" }))
        //    .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
        //    .RuleFor(x => x.IsActive, f => f.Random.Bool())
        //    .RuleFor(x => x.Department, f => f.PickRandom(departments))
        //    .RuleFor(x => x.Education, f => f.PickRandom(educations));

        var accounts = new AccountFaker().Generate(100);
        dbContext.Accounts.AddRange(accounts);
        dbContext.SaveChanges();

        //var products = new ProductFaker().AsTransient().Generate(100);
        //dbContext.Products.AddRange(products);
        //dbContext.SaveChanges();
    }
}