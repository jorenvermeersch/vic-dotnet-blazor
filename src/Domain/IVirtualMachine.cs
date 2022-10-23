﻿namespace Domain;
public interface IVirtualMachine : IMachine{
    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; }
    public ISet<Availability> Availabilities { get; set; }
    public BackupFrequenty BackupFrequenty { get; set; }
    public DateTime ApplicationDate { get; set; }
    public Duration Duration { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public ISet<Port> Ports { get; set; }
}