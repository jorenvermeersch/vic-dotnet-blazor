using Domain.Constants;
using Domain.VirtualMachines;

namespace Domain.Customers;

public class InternalCustomer : Customer
{
    #region Properties
    public Institution Institution { get; set; }
    public string Department { get; set; } = default!;
    public string Education { get; set; } = default!;
    #endregion

    #region Constructors
    private InternalCustomer() { }
    public InternalCustomer(
        Institution institution,
        string department,
        string education,
        ContactPerson contactPerson,
        ContactPerson? backupContact = null,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Institution = institution;
        Department = department;
        Education = education;
    }
    #endregion
}
