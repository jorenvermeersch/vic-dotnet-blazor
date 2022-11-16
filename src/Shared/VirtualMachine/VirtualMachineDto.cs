using Shared.Account;
using Shared.customer;
using Shared.Host;
using Shared.Shared;
using Shared.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public static class VirtualMachineDto
{
    public class Index
    {
        public long Id { get; set; }
        [Required]
        public string FQDN { get; set; }
        [Required]
        public string Status { get; set; }
    }

    public class Details : Index
    {
        [Required]
        public string Name { get; set; }
        public string Template { get; set; }
        [Required]
        public string Mode { get; set; }
        public List<string> Availabilities { get; set; }
        [Required]
        public string BackupFrequenty { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public TimeSpanDto TimeSpan { get; set; } = new TimeSpanDto();
        public string Reason { get; set; }
        public List<PortDto> Ports { get; set; }
        public SpecificationDto Specification { get; set; } = new SpecificationDto();
        public HostDto.Index Host { get; set; } = new HostDto.Index();
        public List<CredentialDto> Credentials { get; set; }
        public AccountDto.Index Account { get; set; } = new AccountDto.Index();
        public CustomerDto.Index Requester { get; set; } = new CustomerDto.Index(); 
        public CustomerDto.Index User { get; set; } = new CustomerDto.Index(); 
    }

    public class Create : Index
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Template { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        //public int hostId { get; set; } = 0;
        public int Processors { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public string Requester { get; set; }
        public string User { get; set; }
        public string Account { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string BackupFrequenty { get; set; }

        public List<CredentialDto> Credentials { get; set; }

    }
    
}
