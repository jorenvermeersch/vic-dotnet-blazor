namespace Domain;

public class InternalCustomer : ICustomer
{
    public string Education { get; set; }
    public string Departement { get; set; }
    public int Id { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }

    public InternalCustomer(string education, string department, ContactPerson contactPerson, ContactPerson backupContact)
    {
        throw new NotImplementedException();
    }
}
