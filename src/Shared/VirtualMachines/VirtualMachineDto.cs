using Domain.Constants;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using System.ComponentModel;

namespace Shared.VirtualMachines;

public static class VirtualMachineDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Fqdn { get; set; } = default!;
        public Status Status { get; set; }
    }

    public class Detail : Index
    {
        [DefaultValue("")]
        public string Name { get; set; } = default!;
        [DefaultValue(Template.AI)]
        public Template Template { get; set; }
        [DefaultValue(Mode.IAAS)]
        public Mode Mode { get; set; }
        public List<string> Availabilities { get; set; } = new();
        [DefaultValue(BackupFrequency.Weekly)]
        public BackupFrequency BackupFrequenty { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public TimeSpanDto TimeSpan { get; set; } = default!;
        [DefaultValue("Reason unknown")]
        public string Reason { get; set; } = default!;
        public List<PortDto> Ports { get; set; } = new();
        public SpecificationsDto Specification { get; set; } = new();
        public HostDto.Index Host { get; set; } = new();
        public List<CredentialsDto> Credentials { get; set; } = new();
        public AccountDto.Index Account { get; set; } = new();
        public CustomerDto.Index Requester { get; set; } = new();
        public CustomerDto.Index User { get; set; } = new();
        public bool hasVpnConnection { get; set; } = false;
    }

    public class Mutate
    {
        public string Fqdn { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Mode? Mode { get; set; }
        public Template? Template { get; set; }
        public string Reason { get; set; } = default!;
        public Status? Status { get; set; }
        public long HostId { get; set; } = default!;
        public SpecificationsDto Specifications { get; set; } = new();
        public long RequesterId { get; set; }
        public long UserId { get; set; }
        public long AdministratorId { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(2);
        public BackupFrequency? BackupFrequency { get; set; }
        public List<CredentialsDto> Credentials { get; set; } = default!;
        public List<int> Ports { get; set; } = new();
        public List<Availability> Availabilities { get; set; } = new();
        public bool hasVpnConnection { get; set; }
    }
}
