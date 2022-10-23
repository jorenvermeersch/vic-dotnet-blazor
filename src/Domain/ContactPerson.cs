namespace Domain;

public class ContactPerson
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public ContactPerson(string firstname, string lastname, string email, string phoneNumber)
    {
        throw new NotImplementedException();
    }
}
