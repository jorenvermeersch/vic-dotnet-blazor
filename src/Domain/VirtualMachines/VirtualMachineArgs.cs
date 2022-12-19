using Domain.Accounts;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;

namespace Domain.VirtualMachines;

public class VirtualMachineArgs
{
    #region Properties
    public string Fqdn { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Mode Mode { get; set; } = default!;
    public Template Template { get; set; } = default!;
    public string Reason { get; set; } = default!;
    public Status Status { get; set; } = default!;
    public Host<VirtualMachine> Host { get; set; } = default!;
    public Specifications Specifications { get; set; } = default!;
    public Customer Requester { get; set; } = default!;
    public Customer User { get; set; } = default!;
    public Account Account { get; set; } = default!;
    public DateTime ApplicationDate { get; set; } = default!;
    public TimeSpan TimeSpan { get; set; } = default!;
    public BackupFrequency BackupFrequency { get; set; } = default!;
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public IList<Port> Ports { get; set; } = new List<Port>();
    public bool HasVpnConnection { get; set; } = false;
    #endregion
}
