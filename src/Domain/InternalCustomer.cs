namespace Domain;

public class InternalCustomer : ICustomer
{
    public string Education { get; set; }
    public Departement Departement { get; set; }
    public int Id { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }

    public InternalCustomer(string education, Departement department, ContactPerson contactPerson, ContactPerson backupContact)
    {
        throw new NotImplementedException();
    }
}
