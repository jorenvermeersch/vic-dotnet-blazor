using Domain.Constants;

namespace Domain.Core;

[ToString]
public class VirtualMachine : Machine
{
    #region Properties
    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; }
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public BackupFrequency BackupFrequency { get; set; }
    public DateTime ApplicationDate { get; set; }
    public Duration TimeSpan { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public IList<Port> Ports { get; set; } = new List<Port>();
    public Host<Machine> Host { get; set; }
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
        Host = args.Host;
        Credentials = args.Credentials;
        Account = args.Account;
        Requester = args.Requester;
        User = args.User;
    }
    #endregion
}
