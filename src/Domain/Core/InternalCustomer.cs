using Ardalis.GuardClauses;

namespace Domain.Domain;

[ToString]
public class InternalCustomer : Customer
{
    #region Properties
    public string Education { get; set; }
    public string Department { get; set; }

    #endregion

    #region Constructors
    public InternalCustomer(
        string education,
        string department,
        ContactPerson contactPerson,
        ContactPerson backupContact,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Education = Guard.Against.InvalidFormat(
            education,
            nameof(education),
            "^.{0}$|^[a-zA-Z]+[ a-zA-Z]*"
        );
        Department = Guard.Against.InvalidFormat(
            department,
            nameof(department),
            "^[a-zA-Z]+[ a-zA-Z]*"
        );
    }
    #endregion
}
