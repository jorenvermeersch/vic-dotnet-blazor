using Ardalis.GuardClauses;
using Domain.Constants;

namespace Domain.Core;

[ToString]
public class InternalCustomer : Customer
{
    #region Properties
    public Institution Institution { get; set; }
    public string Department { get; set; }
    public string Education { get; set; }
    #endregion

    #region Constructors
    public InternalCustomer(
        Institution institution,
        string department,
        string education,
        ContactPerson contactPerson,
        ContactPerson backupContact,
        IList<VirtualMachine>? virtualMachines = null
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Institution = Guard.Against.EnumOutOfRange(institution, nameof(institution));

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
