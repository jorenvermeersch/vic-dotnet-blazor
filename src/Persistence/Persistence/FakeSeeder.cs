using Domain.Core;
using Fakers.Accounts;
using Fakers.VirtualMachines;

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
        SeedVirtualMachines();
    }

    private void SeedAccounts()
    {
        var accounts = new AccountFaker().Generate(100);
        dbContext.Accounts.AddRange(accounts);
        dbContext.SaveChanges();
    }

    private void SeedVirtualMachines()
    {
        var virtualmachines = new VirtualMachineFaker().Generate(20);
        dbContext.VirtualMachines.AddRange(virtualmachines);
        dbContext.SaveChanges();
    }
}