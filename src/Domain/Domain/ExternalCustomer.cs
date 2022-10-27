using Ardalis.GuardClauses;
using Domain.Interfaces;

namespace Domain.Domain;
[ToString]

public class ExternalCustomer : Customer
{
    #region Properties
    public string Name { get; set; }
    public string Type { get; set; }
    #endregion

    #region Construcors
    public ExternalCustomer(string name, string type, ContactPerson contactPerson, ContactPerson backupContact, IList<IVirtualMachine> virtualMachines)
    : base(contactPerson, backupContact, virtualMachines)
    {
        Name = Guard.Against.InvalidFormat(name, nameof(name), "^[a-zA-Z]+[ a-zA-Z]*");
        Type = Guard.Against.InvalidFormat(type, nameof(type), "^[a-zA-Z]+[ a-zA-Z]*");
    }
    #endregion

}
