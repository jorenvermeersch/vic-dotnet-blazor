namespace Domain.Customers;

[ToString]
public class ContactPerson : Entity
{
    #region Properties
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }

    #endregion

    #region Constructors
    private ContactPerson() { }
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
