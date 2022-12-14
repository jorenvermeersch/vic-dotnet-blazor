using Domain.VirtualMachines;

namespace Domain.Customers;

public class ExternalCustomer : Customer
{
    #region Properties
    public string CompanyName { get; set; } = default!;
    public string Type { get; set; } = default!;
    #endregion

    #region Constructors
    private ExternalCustomer() { }

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
