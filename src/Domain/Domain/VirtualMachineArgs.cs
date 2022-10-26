using Domain.Constants;

namespace Domain.Domain;
public class VirtualMachineArgs {
    #region Properties
    public string name { get; set; }
    public int processors { get; set; }
    public int memory { get; set; }
    public int storage { get; set; }
    public Template template { get; set; }
    public Mode mode { get; set; }
    public string fqdn { get; set; }
    public ISet<Availability> availabilities { get; set; }
    public BackupFrequenty backupFrequenty { get; set; }
    public DateTime applicationDate { get; set; }
    public ISet<DateTime> duration { get; set; }
    public Status status { get; set; }
    public string reason { get; set; }
    public IDictionary<int, string> ports { get; set; }
    public int hostId { get; set; }
    public IDictionary<string, string> credentials { get; set; }
    public int accountId { get; set; }
    public int requesterId { get; set; }
    public int userId { get; set; }
    #endregion
    #region Constructors
    public VirtualMachineArgs() {

    }
    #endregion
}
