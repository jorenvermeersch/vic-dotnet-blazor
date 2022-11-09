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
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    )
    {
        ContactPerson = Guard.Against.Null(contactPerson, nameof(contactPerson));

        // TODO: Contact and backupContact cannot have the same contact information.

        BackupContactPerson = backupContact;
        VirtualMachines = virtualMachines ?? new List<VirtualMachine>();
    }
    #endregion
}
