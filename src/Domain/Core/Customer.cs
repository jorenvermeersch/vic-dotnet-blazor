﻿using Ardalis.GuardClauses;

namespace Domain.Core;

public abstract class Customer : Entity
{
    #region Fields
    private ContactPerson _contactPerson;
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
    public IList<VirtualMachine> VirtualMachines { get; set; }
    #endregion

    #region Constructors
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

    //TODO: Validate contact uncomment
    private void ValidateContacts(ContactPerson contactPerson, ContactPerson? backupContact)
    {
        if (contactPerson.HasTheSameContactInformation(backupContact))
        {
            //throw new ArgumentException("Contact person and backup contact should be different");
        }
    }
}
