using Domain.Common;
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
    private readonly int seedValue = 1337;

    public FakeSeeder(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private readonly bool seed = true;

    public void Seed()
    {


        if (seed)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            SeedCustomers();
            SeedAccounts();
            SeedPorts();
            SeedHost();
            SeedVirtualMachines();
            SeedHistory();
        }
    }

    private void SeedAccounts()
    {
        var accounts = new AccountFaker().AsTransient().UseSeed(seedValue).Generate(100);

        dbContext.Accounts.AddRange(accounts);
        dbContext.SaveChanges();
    }

    public void SeedCustomers()
    {
        var contacts = new ContactPersonFaker().AsTransient().UseSeed(seedValue).Generate(75);
        var backupContacts = new ContactPersonFaker(isBackupContact: true).AsTransient().UseSeed(seedValue).Generate(75);
        var internalcustomers = new CustomerFaker.InternalCustomerFaker(contacts, backupContacts).AsTransient().UseSeed(seedValue).Generate(100);
        var externalcustomers = new CustomerFaker.ExternalCustomerFaker(contacts, backupContacts).AsTransient().UseSeed(seedValue).Generate(100);


        dbContext.Customers.AddRange(internalcustomers.Select(
            x => new InternalCustomer(x.Institution, x.Department, x.Education, x.ContactPerson, x.BackupContactPerson)
            ));

        dbContext.Customers.AddRange(externalcustomers.Select(
            x => new ExternalCustomer(x.CompanyName, x.Type, x.ContactPerson, x.BackupContactPerson)
            ));
        dbContext.SaveChanges();
    }

    private void SeedPorts()
    {
        dbContext.Ports.AddRange(
           new List<Port>
           {
                 new Port(number: 443, service: "HTTPS"),
                 new Port(number: 80, service: "HTTP"),
                 new Port(number: 22, "SSH")
           }
        );
        dbContext.SaveChanges();
    }

    private void SeedVirtualMachines()
    {
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
            Ports = dbContext.Ports.ToList(),
        };

        var virtualmachines = vmfaker.AsTransient().UseSeed(1337).Generate(50);
        dbContext.VirtualMachines.AddRange(virtualmachines.Select(x => new VirtualMachine(new VirtualMachineArgs()
        {
            Specifications = new Specifications(x.Specifications.Processors, x.Specifications.Memory, x.Specifications.Storage),
            Credentials = x.Credentials,
            Account = x.Account,
            ApplicationDate = x.ApplicationDate,
            Availabilities = x.Availabilities,
            BackupFrequency = x.BackupFrequency,
            Fqdn = x.Fqdn,
            Name = x.Name,
            Reason = x.Reason,
            HasVpnConnection = x.HasVpnConnection,
            Host = dbContext.Hosts.ToList()[new Random().Next(dbContext.Hosts.ToList().Count)],
            Mode = x.Mode,
            Ports = x.Ports,
            Requester = x.Requester,
            Status = x.Status,
            Template = x.Template,
            TimeSpan = new Domain.VirtualMachines.TimeSpan(x.TimeSpan.StartDate, x.TimeSpan.EndDate),
            User = x.User,
        })));
        dbContext.SaveChanges();
    }

    private void SeedHost()
    {
        var processors = new ProcessorFaker().AsTransient().UseSeed(seedValue).Generate(15);
        dbContext.Processors.AddRange(processors);
        dbContext.SaveChanges();

        var hostSpecifications = new HostSpecificationsFaker(dbContext.Processors.ToList()).UseSeed(seedValue).Generate(40);
        var hosts = new HostFaker(hostSpecifications, dbContext.VirtualMachines.ToList()).AsTransient().UseSeed(seedValue).Generate(25);

        dbContext.Hosts.AddRange(hosts.Select(host =>
            new Server(host.Name, new HostSpecifications(host.Specifications.VirtualisationFactors, host.Specifications.Storage, host.Specifications.Memory), host.Machines)
        ));
        dbContext.SaveChanges();
    }

    private void SeedHistory()
    {
        var history = dbContext.Hosts.ToList().Select(host => new ServerHistory(host));
        dbContext.History.AddRange(history);
        dbContext.SaveChanges();
    }
}