using Ardalis.GuardClauses;

namespace Domain;
public class InternalCustomer : ICustomer
{
    #region Properties
    public int Id { get; set; }
    public string? Education { get; set; }
    public string Departement { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson? BackupContactPerson { get; set; }
    public IList<IVirtualMachine>? VirtualMachines { get; set; }
    #endregion

    #region Constructors
    public InternalCustomer(string education, string department, ContactPerson contactPerson, ContactPerson? backupContact = null) {
        Education = Guard.Against.InvalidFormat(education, nameof(education), "^.{0}$|^[a-zA-Z]+[ a-zA-Z]*");
        Departement = Guard.Against.InvalidFormat(department, nameof(department), "^[a-zA-Z]+[ a-zA-Z]*");
        ContactPerson = contactPerson;
        BackupContactPerson = backupContact;
    }
    #endregion
}
