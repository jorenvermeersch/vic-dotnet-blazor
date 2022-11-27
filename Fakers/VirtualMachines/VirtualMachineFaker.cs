using BogusStore.Fakers.Common;
using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Domain.VirtualMachines;
using Fakers.Accounts;
using Fakers.Credentials;
using Fakers.Customers;
using Fakers.Specifications;
using Fakers.TimeSpan;

namespace Fakers.VirtualMachines;

public class VirtualMachineFaker : EntityFaker<VirtualMachine>
{
    public VirtualMachineFaker(string locale = "nl")
    {
        CustomerFaker.ExternalCustomerFaker externalCustomerFaker = new();
        CustomerFaker.InternalCustomerFaker internalCustomerFaker = new();

        List<Customer> customers = new();
        customers.AddRange(externalCustomerFaker.Generate(20));
        customers.AddRange(internalCustomerFaker.Generate(20));

        AccountFaker accountFaker = new();
        List<Account> accounts = accountFaker.Generate(20);

        CredentialFaker credentialFaker = new();

        List<VirtualMachine> virtualMachines = new();

        List<Port> ports = new();
        ports.AddRange(
            new List<Port>
            {
                new Port(number: 443, service: "HTTPS"),
                new Port(number: 80, service: "HTTP"),
                new Port(number: 22, "SSH")
            }
        );

        TimeSpanFaker timeSpanFaker = new();
        List<Domain.VirtualMachines.TimeSpan> timespans = timeSpanFaker.Generate(5);

        SpecificationsFaker specificationFaker = new();
        List<Domain.Common.Specifications> specifications = specificationFaker.Generate(10);

        CustomInstantiator(f => new VirtualMachine(new VirtualMachineArgs
        {
            // EASY TO INITIALIZE
            Name = f.Internet.DomainWord(),
            Fqdn = f.Internet.DomainName(),
            Reason = f.PickRandom(new[] { "Bachelor proef", "AI trainen", "Database draaien" }),

            // ENUMS
            Template = f.PickRandom(Enum.GetValues(typeof(Template)).Cast<Template>().ToList()),
            Mode = f.PickRandom(Enum.GetValues(typeof(Mode)).Cast<Mode>().ToList()),
            Availabilities = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(Enum.GetValues(typeof(Availability)).Cast<Availability>().ToList())).ToList(),
            BackupFrequency = f.PickRandom(Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToList()),
            Status = f.PickRandom(Enum.GetValues(typeof(Status)).Cast<Status>().ToList()),

            // OTHER FAKERS
            User = f.PickRandom(customers),
            Requester = f.PickRandom(customers),
            Account = f.PickRandom(accounts),
            Credentials = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(credentialFaker.Generate(25))).ToList(),
            Host = null,
            Ports = Enumerable.Range(1, f.Random.Int(1, 2)).Select(x => f.PickRandom(ports)).ToList(),
            TimeSpan = f.PickRandom(timespans),
            Specifications = f.PickRandom(specifications)
        }));
    }
}
