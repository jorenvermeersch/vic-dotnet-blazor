using Bogus;
using Domain.Accounts;
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

namespace Services.FakeInitializer;

public class FakeInitializerService : IFakeInitializerService
{
    private static readonly int seed = 1337;

    public static List<VirtualMachine>? FakeVirtualMachines { get; set; } = new();
    public static List<Server>? FakeHosts { get; set; } = new();
    public static List<Server>? FakeHostsVirtualMachines { get; set; } = new();
    public static List<ContactPerson>? FakeContactPersons { get; set; } = new();
    public static List<ContactPerson>? FakeBackupContactPersons { get; set; } = new();
    public static List<HostSpecifications>? FakeHostSpecifications { get; set; } = new();
    public static List<Account>? FakeAccounts { get; set; } = new();
    public static List<Processor>? FakeProcessors { get; set; } = new();
    public static List<Credentials>? FakeCredentials { get; set; } = new();
    public static List<Domain.VirtualMachines.TimeSpan>? FakeTimeSpans { get; set; } = new();
    public static List<Specifications>? FakeSpecifications { get; set; } = new();
    public static List<Customer>? FakeCustomers { get; set; } = new();

    public FakeInitializerService()
    {
        List<Port> Ports = new()
           {
                 new Port(number: 443, service: "HTTPS"),
                 new Port(number: 80, service: "HTTP"),
                 new Port(number: 22, "SSH")
           };

        FakeSpecifications = new SpecificationsFaker().UseSeed(seed).Generate(20);
        FakeTimeSpans = new TimeSpanFaker().UseSeed(seed).Generate(20);
        FakeCredentials = new CredentialFaker().UseSeed(seed).Generate(20);
        FakeContactPersons = new ContactPersonFaker().UseSeed(seed).Generate(50);
        FakeBackupContactPersons = new ContactPersonFaker(isBackupContact: true).UseSeed(seed).Generate(50);
        FakeCustomers.AddRange(new CustomerFaker.ExternalCustomerFaker(FakeContactPersons, FakeBackupContactPersons).UseSeed(seed + 1).Generate(20));
        FakeCustomers.AddRange(new CustomerFaker.InternalCustomerFaker(FakeContactPersons, FakeBackupContactPersons, id: FakeCustomers.Count() + 1).UseSeed(seed).Generate(25));
        FakeAccounts = new AccountFaker().UseSeed(seed).Generate(25);
        FakeProcessors = new ProcessorFaker().UseSeed(seed).Generate(25);
        FakeHostSpecifications = new HostSpecificationsFaker(FakeProcessors).UseSeed(seed).Generate(20);

        VirtualMachineFaker vmFaker = new()
        {
            Customers = FakeCustomers,
            Accounts = FakeAccounts,
            Specifications = FakeSpecifications,
            Credentials = FakeCredentials,
            TimeSpans = FakeTimeSpans,
            Hosts = FakeHosts,
            Ports = Ports,
        };
        FakeVirtualMachines = vmFaker.UseSeed(seed).Generate(30);

        FakeHosts = new HostFaker(FakeHostSpecifications, FakeVirtualMachines).UseSeed(seed).Generate(25);

        FakeHostsVirtualMachines = new HostFaker(FakeHostSpecifications, new List<VirtualMachine>()).UseSeed(seed).Generate(20);

        foreach (var item in FakeVirtualMachines)
        {
            item.Host = new Faker().PickRandom(FakeHostsVirtualMachines);
        }
    }
}
