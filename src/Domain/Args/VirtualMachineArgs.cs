using Domain.Constants;

namespace Domain.Args;

public class VirtualMachineArgs
{
    #region Properties
    public string name { get; set; }
    public int processors { get; set; }
    public int memory { get; set; }
    public int storage { get; set; }
    public Template template { get; set; }
    public Mode mode { get; set; }
    public string fqdn { get; set; }
    public ISet<Availability> availabilities { get; set; }
    public BackupFrequency backupFrequency { get; set; }
    public DateTime applicationDate { get; set; }
    public ISet<DateTime> timeSpan { get; set; }
    public Status status { get; set; }
    public string reason { get; set; }
    public IDictionary<int, string> ports { get; set; }
    public string hostName { get; set; }
    public IDictionary<string, string> credentials { get; set; }
    public string accountEmail { get; set; }
    public string requesterEmail { get; set; }
    public string userEmail { get; set; }
    #endregion
}
