﻿using Domain.Accounts;
using Fakers.Accounts;

namespace Persistence.Data;

public class FakeSeeder
{
    private readonly VicDbContext dbContext;

    public FakeSeeder(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private readonly bool seed = true;

    public void Seed()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        if (seed)
        {
            SeedCustomers();
            SeedAccounts();
            SeedVirtualMachines();
        }
    }

    private void SeedAccounts()
    {
        var accounts = new AccountFaker().UseSeed(1337).Generate(100);

        dbContext.Accounts.AddRange(
            accounts.Select(
                x => new Account
                {
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    Role = x.Role,
                    PasswordHash = x.PasswordHash,
                    IsActive = x.IsActive,
                    Department = x.Department,
                    Education = x.Education
                }
                )
            );
        dbContext.SaveChanges();
    }

    public void SeedCustomers()
    {
        //TODO: Seeding database fix CUSTOMERS

        //var internalcustomers = new CustomerFaker.InternalCustomerFaker().UseSeed(1337).Generate(100);
        //var externalcustomers = new CustomerFaker.ExternalCustomerFaker().UseSeed(1337).Generate(100);

        //dbContext.InternalCustomers.AddRange(internalcustomers.Select(x => new Domain.Customers.InternalCustomer
        //{
        //    Department = x.Department,
        //    ContactPerson = x.ContactPerson,
        //    BackupContactPerson = x.BackupContactPerson,
        //    Institution = x.Institution,
        //    Education = x.Education
        //}));

        //dbContext.ExternalCustomers.AddRange(externalcustomers.Select(x => new Domain.Customers.ExternalCustomer
        //{
        //    CompanyName = x.CompanyName,
        //    Type = x.Type,
        //    ContactPerson = x.ContactPerson,
        //    BackupContactPerson = x.BackupContactPerson,
        //}));


        //dbContext.InternalCustomers.AddRange((IEnumerable<Domain.Customers.InternalCustomer>)internalcustomers);
        //dbContext.InternalCustomers.AddRange(internalcustomers.Select(x => new InternalCustomer() { ContactPerson = x.ContactPerson, BackupContactPerson = x.BackupContactPerson }));

        dbContext.SaveChanges();
    }

    private void SeedVirtualMachines()
    {
        //TODO: Seeding database fix VMS

        //var virtualmachines = new VirtualMachineFaker().UseSeed(1337).Generate(20);
        //dbContext.VirtualMachines.AddRange(virtualmachines.Select(x => new VirtualMachine { Fqdn = x.Fqdn, Name = x.Name }));
        dbContext.SaveChanges();
    }
}