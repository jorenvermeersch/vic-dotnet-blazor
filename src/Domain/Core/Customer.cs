namespace Domain.Domain;

public class Customer : Entity
{
    #region Properties
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson? BackupContactPerson { get; set; }
    public IList<VirtualMachine> VirtualMachines { get; set; }
    #endregion

    #region Constructors
    public Customer(
        ContactPerson contactPerson,
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    )
    {
        ContactPerson = contactPerson;
        BackupContactPerson = backupContact;
        VirtualMachines = virtualMachines ?? new List<VirtualMachine>();
    }
    #endregion
}
