using Ardalis.GuardClauses;

namespace Domain.Domain;
[ToString]
public class ContactPerson
{
    #region Properties
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    #endregion

    #region Constructors
    public ContactPerson(string firstname, string lastname, string email, string phoneNumber)
    {
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(firstname), "[^0-9\\\\W]+");
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(lastname), "[^0-9\\W]+");
        Email = Guard.Against.InvalidFormat(email, nameof(email), "^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-z]+)$");
        PhoneNumber = Guard.Against.InvalidFormat(phoneNumber, nameof(phoneNumber), "[+]*[0-9]+");
    }
    #endregion
}
