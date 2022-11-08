using Ardalis.GuardClauses;
using Domain.Constants;
using System.Text.RegularExpressions;

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
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(Firstname), Validation.Name);
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(Lastname), Validation.Name);
        Email = Guard.Against.InvalidFormat(email, nameof(Email), Validation.Email);
        PhoneNumber = Guard.Against.NullOrInvalidInput(
            phoneNumber,
            nameof(PhoneNumber),
            input => Regex.IsMatch(input ?? "", Validation.PhoneNumber)
        );
    }
    #endregion
}
