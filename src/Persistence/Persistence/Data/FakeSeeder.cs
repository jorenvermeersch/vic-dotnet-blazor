using Domain.Core;
using Fakers.Accounts;
using Fakers.VirtualMachines;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class FakeSeeder
{
    private readonly VicDBContext dbContext;

    public FakeSeeder(VicDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        dbContext.Database.EnsureDeleted();
        if (dbContext.Database.EnsureCreated())
        {
            SeedAccounts();
            SeedVirtualMachines();
        }
    }

    private void SeedAccounts()
    {

        var accounts = new AccountFaker().UseSeed(1337).Generate(100);
        dbContext.Accounts.AddRange(accounts.Select(x => new Account { Firstname = x.Firstname, Lastname = x.Lastname, Email = x.Email, Role = x.Role, PasswordHash = x.PasswordHash, IsActive = x.IsActive, Department = x.Department, Education = x.Education }));
        dbContext.SaveChanges();
    }

    private void SeedVirtualMachines()
    {
        var virtualmachines = new VirtualMachineFaker().UseSeed(1337).Generate(20);
        dbContext.VirtualMachines.AddRange(virtualmachines.Select(x => new VirtualMachine { Fqdn = x.Fqdn, Name = x.Name }));
        dbContext.SaveChanges();
    }
}