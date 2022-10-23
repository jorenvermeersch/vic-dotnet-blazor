namespace Domain;

public class ExternalCustomer : ICustomer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }

    public ExternalCustomer(string name, string type, ContactPerson contactPerson, ContactPerson backupContact)
    {
        throw new NotImplementedException();
    }
}
