using Shared.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.customer;

public static class CustomerDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Details
    {
        public long Id { get; set; }
        public string Institution { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Education { get; set; }
        public string Department { get; set; }
        public ContactPersonDto ContactPerson { get; set; }
        public ContactPersonDto BackupContactPerson { get; set; }
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; }
    }
}
