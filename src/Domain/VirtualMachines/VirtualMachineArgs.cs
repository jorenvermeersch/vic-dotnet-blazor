﻿using Domain.Administrators;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;

namespace Domain.VirtualMachines;

public class VirtualMachineArgs
{
    #region Properties
    public string Name { get; set; }
    public Specifications Specifications { get; set; }
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
    public Host<VirtualMachine> Host { get; set; }
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public Account Account { get; set; }
    public Customer Requester { get; set; }
    public Customer User { get; set; }
    #endregion
}