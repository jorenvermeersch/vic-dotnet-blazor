using Ardalis.GuardClauses;
using Domain.Accounts;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;

namespace Domain.VirtualMachines;

public class VirtualMachine : Machine
{
    #region Fields
    private Server _host = default!;
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
        }
    }

    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; } = default!;
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public BackupFrequency BackupFrequency { get; set; }
    public DateTime ApplicationDate { get; set; }
    public TimeSpan TimeSpan { get; set; } = default!;
    public Status Status { get; set; }
    public string Reason { get; set; } = default!;
    public IList<Port> Ports { get; set; } = new List<Port>();

    public Server Host
    {
        get => _host;
        set
        {
            Guard.Against.Null(value, nameof(Host));

            if (_host != value)
            {
                value.AddMachine(this); // Throws if new host does not have enough remaining resources.

                if (_host is not null)
                {
                    _host.RemoveMachine(this); // Remove from old host.
                }
                _host = value;
            }
        }
    }
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public Account Account { get; set; } = default!;
    public Customer Requester { get; set; } = default!;
    public Customer User { get; set; } = default!;
    public bool HasVpnConnection { get; set; }
    #endregion

    #region Constructors

    private VirtualMachine() { }

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
        HasVpnConnection = args.HasVpnConnection;
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
