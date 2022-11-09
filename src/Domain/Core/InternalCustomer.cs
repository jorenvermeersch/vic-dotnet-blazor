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
        IList<VirtualMachine>? virtualMachines
    ) : base(contactPerson, backupContact, virtualMachines)
    {
        Institution = Guard.Against.EnumOutOfRange(institution, nameof(institution));
        Education = Guard.Against.NullOrInvalidInput(
            education,
            nameof(Education),
            input => Validation.Education.IsMatch(input)
        );
        Department = Guard.Against.NullOrInvalidInput(
            department,
            nameof(Department),
            input => Validation.Departement.IsMatch(input)
        );
    }
    #endregion
}
