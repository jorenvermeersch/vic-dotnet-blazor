namespace Domain;
public class ContactPerson
{
    #region Properties
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    #endregion

    #region Constructors
    public ContactPerson(string firstname, string lastname, string email, string phoneNumber) {
        throw new NotImplementedException();
    }
    #endregion
}
