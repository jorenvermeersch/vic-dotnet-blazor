using Domain.Constants;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;

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
        public string Name { get; set; } = default!;
        public Template Template { get; set; }
        public Mode Mode { get; set; }
        public List<string> Availabilities { get; set; } = default!;
        public BackupFrequency BackupFrequenty { get; set; }
        public DateTime ApplicationDate { get; set; } = default!;
        public TimeSpanDto TimeSpan { get; set; } = default!;
        public string Reason { get; set; } = default!;
        public List<PortDto> Ports { get; set; } = default!;
        public SpecificationsDto Specification { get; set; } = default!;
        public HostDto.Index Host { get; set; } = default!;
        public List<CredentialsDto> Credentials { get; set; } = default!;
        public AccountDto.Index Account { get; set; } = default!;
        public CustomerDto.Index Requester { get; set; } = default!;
        public CustomerDto.Index User { get; set; } = default!;
        public bool hasVpnConnection { get; set; }
    }

    public class Mutate
    {
        public string Fqdn { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Mode Mode { get; set; } = default!;
        public Template Template { get; set; } = default!;
        public string Reason { get; set; } = default!;
        public Status Status { get; set; } = default!;
        public long HostId { get; set; } = default!;
        public SpecificationsDto Specifications { get; set; } = new();
        public long RequesterId { get; set; }
        public long UserId { get; set; }
        public long AdministratorId { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public BackupFrequency BackupFrequency { get; set; } = default!;
        public List<CredentialsDto> Credentials { get; set; } = default!;
        public List<int> Ports { get; set; } = default!;
        public List<Availability> Availabilities = default!;
        public bool hasVpnConnection { get; set; }
    }
}
