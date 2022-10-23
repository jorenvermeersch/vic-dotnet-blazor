namespace Domain;

public interface ICustomer
{
    public int Id { get; set; }

    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }
}
