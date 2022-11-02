using Ardalis.GuardClauses;

namespace Domain.Core;

[ToString]
public class ExternalCustomer : Customer
{
    #region Properties
    public string Name { get; set; }
    public string Type { get; set; }
    #endregion

    #region Construcors
    public ExternalCustomer(
        string name,
        string type,
        ContactPerson contactPerson,
        ContactPerson backupContact,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Type = Guard.Against.NullOrWhiteSpace(type, nameof(type));
    }
    #endregion
}
