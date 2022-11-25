using Domain.Constants;
using Shared.VirtualMachine;

namespace Shared.Customer;

public static class CustomerDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; } = default!;
    }

    public class Details
    {
        public long Id { get; set; }
        public Institution? Institution { get; set; }
        public string? Department { get; set; }
        public string? Education { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? CompanyType { get; set; }
        public string? CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = default!;
        public ContactPersonDto? BackupContactPerson { get; set; }
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; } = new();
    }
    public class Create
    {
        public string CustomerType { get; set; } = default!;
        public string? Institution { get; set; }
        public string? Department { get; set; }
        public string? CompanyType { get; set; }
        public string? Education { get; set; }
        public string? CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = default!;
        public ContactPersonDto? BackupContactPerson { get; set; }
    }
}
