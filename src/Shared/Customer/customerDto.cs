using Domain.Constants;
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
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; }
    }

    public class Details {
        public long Id { get; set; }
        public Institution? Institution { get; set; }
        public string Department { get; set; }
        public string CompanyType { get; set; }
        public string Education { get; set; }
        public CustomerType CustomerType { get; set; }
        //Name from external customer company
        public string CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; }
        public ContactPersonDto BackupContactPerson { get; set; }
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; }
    }
    public class Create
    {
        public string Email { get; set; }
        public string CustomerType { get; set; }
        public string Institution { get; set; }
        public string Department { get; set; }
        public string CompanyType { get; set; }
        public string Education { get; set; }
        public string CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = new();
        public ContactPersonDto BackupContactPerson { get; set; }=new();
    }
}
