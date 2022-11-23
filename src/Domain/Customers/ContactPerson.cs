namespace Domain.Customers;

public class ContactPerson : Entity
{
    #region Properties
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }

    #endregion

    #region Constructors
    public ContactPerson(
        string firstname,
        string lastname,
        string email,
        string? phoneNumber = null
    )
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    #endregion

    public bool HasTheSameContactInformation(ContactPerson? other)
    {
        if (other is null)
            return false;

        if (Email == other.Email)
            return true;

        if (
            PhoneNumber is not null
            && other.PhoneNumber is not null
            && PhoneNumber == other.PhoneNumber
        )
            return true;

        return false;
    }
}
