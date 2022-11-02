using Ardalis.GuardClauses;
using Domain.Constants;

namespace Domain.Core;

[ToString]
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
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(firstname), Validation.Name);
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(lastname), Validation.Name);
        Email = Guard.Against.InvalidFormat(email, nameof(email), Validation.Email);

        if (phoneNumber != null)
        {
            PhoneNumber = Guard.Against.InvalidFormat(
                phoneNumber,
                nameof(phoneNumber),
                Validation.PhoneNumber
            );
        }
    }
    #endregion
}
