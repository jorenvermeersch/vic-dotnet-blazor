using Domain.VirtualMachines;

namespace Domain.Customers;

[ToString]
public class ExternalCustomer : Customer
{
    #region Properties
    public string CompanyName { get; set; }
    public string Type { get; set; }
    #endregion

    #region Constructors
    public ExternalCustomer(
        string name,
        string type,
        ContactPerson contactPerson,
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        CompanyName = name;
        Type = type;
    }
    #endregion
}
