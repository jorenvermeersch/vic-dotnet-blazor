using Ardalis.GuardClauses;
using Domain.Interfaces;

namespace Domain.Domain;
[ToString]

public class ExternalCustomer : ICustomer
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson? BackupContactPerson { get; set; }
    public IList<IVirtualMachine>? VirtualMachines { get; set; }
    #endregion
    #region Construcors
    public ExternalCustomer(string name, string type, ContactPerson contactPerson, ContactPerson? backupContact = null)
    {
        Name = Guard.Against.InvalidFormat(name, nameof(name), "^[a-zA-Z]+[ a-zA-Z]*");
        Type = Guard.Against.InvalidFormat(type, nameof(type), "^[a-zA-Z]+[ a-zA-Z]*");
        ContactPerson = contactPerson;
        BackupContactPerson = backupContact;
    }
    #endregion

}
