using Bogus;
using Domain.Accounts;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Accounts;
using Fakers.ContactPersons;
using Fakers.Credentials;
using Fakers.Customers;
using Fakers.Hosts;
using Fakers.Processors;
using Fakers.Specifications;
using Fakers.TimeSpan;
using Fakers.VirtualMachines;

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
            SeedHost();
            SeedVirtualMachines();
        }
    }

    private void SeedAccounts()
    {
        var accounts = new AccountFaker().AsTransient().UseSeed(1337).Generate(100);

        dbContext.Accounts.AddRange(accounts);
        dbContext.SaveChanges();
    }

    public void SeedCustomers()
    {
        var contacts = new ContactPersonFaker().AsTransient().UseSeed(1337).Generate(150);
        var internalcustomers = new CustomerFaker.InternalCustomerFaker(contacts).AsTransient().UseSeed(1337).Generate(100);
        var externalcustomers = new CustomerFaker.ExternalCustomerFaker(contacts).AsTransient().UseSeed(1337).Generate(100);


        dbContext.Customers.AddRange(internalcustomers.Select(x => new InternalCustomer(x.Institution, x.Department, x.Education, x.ContactPerson, x.BackupContactPerson)));

        dbContext.Customers.AddRange(externalcustomers.Select(x => new ExternalCustomer(x.CompanyName, x.Type, x.ContactPerson, x.BackupContactPerson)));

        dbContext.SaveChanges();
    }

    private void SeedVirtualMachines()
    {
        //TODO: Seeding database fix VMS
       // dbContext.Ports.AddRange(
       //    new List<Port>
       //    {
       //         new Port(number: 443, service: "HTTPS"),
       //         new Port(number: 80, service: "HTTP"),
       //         new Port(number: 22, "SSH")
       //    }
       //);
        var creadentials = new CredentialFaker().AsTransient().UseSeed(1337).Generate(150);
        var timespans = new TimeSpanFaker().UseSeed(1337).Generate(50);
        var specifications = new SpecificationsFaker().UseSeed(1337).Generate(20);

        var vmfaker = new VirtualMachineFaker()
        {
            Customers = dbContext.Customers.ToList(),
            Accounts = dbContext.Accounts.ToList(),
            Specifications = specifications,
            Credentials = creadentials,
            TimeSpans = timespans,
        };

        var virtualmachines = vmfaker.AsTransient().UseSeed(1337).Generate(50);
        dbContext.VirtualMachines.AddRange(virtualmachines.Select(x =>new VirtualMachine(new VirtualMachineArgs()
        {
            Specifications = new Domain.Common.Specifications(x.Specifications.Processors, x.Specifications.Memory, x.Specifications.Storage),
            Credentials = x.Credentials,
            Account= x.Account,
            ApplicationDate= x.ApplicationDate,
            Availabilities= x.Availabilities,
            BackupFrequency= x.BackupFrequency,
            Fqdn = x.Fqdn,
            Name= x.Name,
            Reason= x.Reason,
            HasVpnConnection= x.HasVpnConnection,
           // Host= x.Host,
            Mode= x.Mode,
            Ports= x.Ports,
            Requester = x.Requester,
            Status= x.Status,
            Template= x.Template,
            TimeSpan = x.TimeSpan,
            User = x.User
        })));
        dbContext.SaveChanges();
    }

    private void SeedHost()
    {
        var processors = new ProcessorFaker().AsTransient().UseSeed(1337).Generate(15);
        dbContext.Processors.AddRange(processors);

        var HostSpecifications = new HostSpecificationsFaker().UseSeed(1337).Generate(50);
        var fakeHosts = new HostFaker(HostSpecifications, dbContext.VirtualMachines.ToList()).UseSeed(1337).Generate(25);

        dbContext.Hosts.AddRange(fakeHosts);
        dbContext.SaveChanges();

        
    }
}