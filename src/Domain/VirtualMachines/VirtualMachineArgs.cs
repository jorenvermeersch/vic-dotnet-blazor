using Domain.Accounts;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;

namespace Domain.VirtualMachines;

public class VirtualMachineArgs
{
    #region Properties
    public string Fqdn { get; set; }
    public string Name { get; set; }
    public Mode Mode { get; set; }
    public Template Template { get; set; }
    public string Reason { get; set; }
    public Status Status { get; set; }
    public Host<VirtualMachine> Host { get; set; }
    public Specifications Specifications { get; set; }
    public Customer Requester { get; set; }
    public Customer User { get; set; }
    public Account Account { get; set; }
    public DateTime ApplicationDate { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public BackupFrequency BackupFrequency { get; set; }
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public IList<Port> Ports { get; set; } = new List<Port>();
    public bool HasVpnConnection { get; set; } = false;
    #endregion
}
