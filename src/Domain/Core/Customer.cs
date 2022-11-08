using Ardalis.GuardClauses;

namespace Domain.Core;

public abstract class Customer : Entity
{
    #region Properties
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson? BackupContactPerson { get; set; }
    public IList<VirtualMachine> VirtualMachines { get; set; }
    #endregion

    #region Constructors
    public Customer(
        ContactPerson contactPerson,
        ContactPerson? backupContact,
        IList<VirtualMachine>? virtualMachines
    )
    {
        ContactPerson = Guard.Against.Null(contactPerson, nameof(ContactPerson));
        BackupContactPerson = backupContact;
        VirtualMachines = virtualMachines ?? new List<VirtualMachine>();
    }
    #endregion
}
