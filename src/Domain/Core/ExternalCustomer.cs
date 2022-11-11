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
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Name = name;
        Type = type;
    }
    #endregion
}
