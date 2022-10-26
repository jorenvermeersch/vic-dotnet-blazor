using Domain.Domain;

namespace Domain.Interfaces;

public interface ICustomer
{
    public int Id { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }
    //public IList<IVirtualMachine> VirtualMachines { get; set; }
}
