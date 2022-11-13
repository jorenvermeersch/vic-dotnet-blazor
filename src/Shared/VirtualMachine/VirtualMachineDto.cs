﻿using Shared.Account;
using Shared.customer;
using Shared.Host;
using Shared.Shared;
using Shared.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public static class VirtualMachineDto
{
    public class Index
    {
        public long Id { get; set; }
        public string FQDN { get; set; }
        public string Status { get; set; }
    }

    public class Details : Index
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public string Mode { get; set; }
        public List<string> Availabilities { get; set; } 
        public string BackupFrequenty { get; set; }
        public DateTime ApplicationDate { get; set; }
        public TimeSpanDto TimeSpan { get; set; }
        public string Reason { get; set; }
        public List<PortDto> Ports { get; set; }
        public SpecificationDto Specification { get; set; }
        public HostDto.Index Host { get; set; } 
        public List<CredentialDto> Credentials { get; set; } 
        public AccountDto.Index Account { get; set; } 
        public CustomerDto.Index Requester { get; set; } 
        public CustomerDto.Index User { get; set; } 
    }
    
}
