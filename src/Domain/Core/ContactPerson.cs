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
    public ContactPerson(string firstname, string lastname, string email, string? phoneNumber)
    {
        Firstname = Guard.Against.NullOrInvalidInput(firstname, nameof(firstname), input => Validation.Name.IsMatch(input));
        Lastname = Guard.Against.NullOrInvalidInput(lastname, nameof(lastname), input => Validation.Name.IsMatch(input));
        Email = Guard.Against.NullOrInvalidInput(email, nameof(email), input => Validation.Email.IsMatch(input));
        PhoneNumber = Guard.Against.NullOrInvalidInput(
            phoneNumber,
            nameof(PhoneNumber),
            input => Validation.PhoneNumber.IsMatch(input ?? "")
        );
    }
    #endregion
}
