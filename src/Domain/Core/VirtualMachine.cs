using Ardalis.GuardClauses;
using Domain.Constants;

namespace Domain.Core;

//[ToString]
public class VirtualMachine : Machine
{
    #region Fields
    private Host<VirtualMachine> _host;
    #endregion


    #region Properties
    public new Specifications Specifications
    {
        get => base.Specifications;
        set
        {
            Specifications increase = CalculateResourceIncrease(value);

            if (!_host.RemainingResources.HasResourcesFor(increase))
            {
                throw new ArgumentException(
                    $"{_host.GetType().Name} {_host.Name} cannot accommodate the increase in resources"
                );
            }

            base.Specifications = value;
            _host.UpdateRemainingResources(); // Host cannot detect itself when the required resources of the virtual machines it houses change.
        }
    }

    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; }
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public BackupFrequency BackupFrequency { get; set; }
    public DateTime ApplicationDate { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public IList<Port> Ports { get; set; } = new List<Port>();
    public Host<VirtualMachine> Host
    {
        get => _host;
        set
        {
            Guard.Against.Null(value, nameof(Host));

            if (!_host.Equals(value))
            {
                value.Add(this); // Throws if new host does not have enough remaining resources.
                _host.Remove(this); // Remove from old host.
                _host = value;
            }
        }
    }
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public Account Account { get; set; }
    public Customer Requester { get; set; }
    public Customer User { get; set; }
    #endregion

    #region Constructors
    public VirtualMachine(VirtualMachineArgs args) : base(args.Name, args.Specifications)
    {
        Template = args.Template;
        Mode = args.Mode;
        Fqdn = args.Fqdn;
        Availabilities = args.Availabilities;
        BackupFrequency = args.BackupFrequency;
        ApplicationDate = args.ApplicationDate;
        TimeSpan = args.TimeSpan;
        Status = args.Status;
        Reason = args.Reason;
        Ports = args.Ports;
        _host = args.Host;
        Credentials = args.Credentials;
        Account = args.Account;
        Requester = args.Requester;
        User = args.User;

        _host.Add(this); // Remaining resources host are automatically updated.
    }
    #endregion

    #region Methods
    private Specifications CalculateResourceIncrease(Specifications newSpecifications)
    {
        int processorIncrease,
            memoryIncrease,
            storageIncrease;

        // Decrease in resources is always possible. It is represented by the value zero to make check logic easier.
        processorIncrease = Math.Max(0, newSpecifications.Processors - Specifications.Processors);
        memoryIncrease = Math.Max(0, newSpecifications.Memory - Specifications.Memory);
        storageIncrease = Math.Max(0, newSpecifications.Storage - Specifications.Storage);

        return new Specifications(processorIncrease, memoryIncrease, storageIncrease);
    }
    #endregion
}
