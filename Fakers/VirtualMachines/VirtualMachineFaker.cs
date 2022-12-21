using BogusStore.Fakers.Common;
using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;

namespace Fakers.VirtualMachines;

public class VirtualMachineFaker : EntityFaker<VirtualMachine>
{
    public List<Customer>? Customers { get; set; } = new();
    public List<Account>? Accounts { get; set; } = new();
    public List<Domain.Common.Specifications>? Specifications { get; set; } = new();
    public List<Domain.VirtualMachines.Credentials>? Credentials { get; set; } = new();
    public List<Domain.VirtualMachines.TimeSpan>? TimeSpans { get; set; } = new();
    public List<Server> Hosts { get; set; } = new();
    public List<Port> Ports { get; set; } = new();




    public VirtualMachineFaker(string locale = "nl")
    {

        CustomInstantiator(f => new VirtualMachine(new VirtualMachineArgs
        {
            // Easy to initialize. 
            Name = f.Internet.DomainWord(),
            Fqdn = f.Internet.DomainName(),
            Reason = f.PickRandom(new[] { "Bachelor proef", "AI trainen", "Database draaien" }),

            // Enums. 
            Template = f.PickRandom(Enum.GetValues(typeof(Template)).Cast<Template>().ToList()),
            Mode = f.PickRandom(Enum.GetValues(typeof(Mode)).Cast<Mode>().ToList()),
            Availabilities = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(Enum.GetValues(typeof(Availability)).Cast<Availability>().ToList())).ToList(),
            BackupFrequency = f.PickRandom(Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToList()),
            Status = f.PickRandom(Enum.GetValues(typeof(Status)).Cast<Status>().ToList()),

            //// Other fakers. 
            User = f.PickRandom(Customers),
            Requester = f.PickRandom(Customers),
            Account = f.PickRandom(Accounts),
            Credentials = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(Credentials)).ToList(),
            Host = null, // Host gets initialized later on.
            Ports = Enumerable.Range(1, f.Random.Int(1, 2)).Select(x => f.PickRandom(Ports)).ToList(),
            TimeSpan = f.PickRandom(TimeSpans),
            Specifications = f.PickRandom(Specifications)
        }));
    }
}
