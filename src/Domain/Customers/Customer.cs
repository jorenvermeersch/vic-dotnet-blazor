using Ardalis.GuardClauses;
using Domain.VirtualMachines;

namespace Domain.Customers;

public abstract class Customer : Entity
{
    #region Fields
    private ContactPerson _contactPerson = default!;
    private ContactPerson? _backupContactPerson;
    #endregion

    #region Properties
    public ContactPerson ContactPerson
    {
        get => _contactPerson;
        set
        {
            Guard.Against.Null(value, nameof(ContactPerson));

            ValidateContacts(value, BackupContactPerson);
            _contactPerson = value;
        }
    }

    public ContactPerson? BackupContactPerson
    {
        get => _backupContactPerson;
        set
        {
            ValidateContacts(ContactPerson, value);
            _backupContactPerson = value;
        }
    }

    public IList<VirtualMachine> VirtualMachines { get; set; } = default!;
    #endregion

    #region Constructors
    protected Customer() { }

    public Customer(
        ContactPerson contactPerson,
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    )
    {
        ValidateContacts(contactPerson, backupContact);
        _contactPerson = contactPerson;
        _backupContactPerson = backupContact;
        VirtualMachines = virtualMachines ?? new List<VirtualMachine>();
    }
    #endregion

    private void ValidateContacts(ContactPerson contactPerson, ContactPerson? backupContact)
    {
        if (contactPerson.HasTheSameContactInformation(backupContact))
        {
            // TODO: Uncomment after implementing database mapping.
            throw new ArgumentException("Contact person and backup contact should be different");
        }
    }
}
