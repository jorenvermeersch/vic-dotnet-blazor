using Ardalis.GuardClauses;
using Domain.Constants;

namespace Domain.Core;

[ToString]
public class Credentials
{
    #region Properties
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // Chosen by administrator. Infinite options.
    #endregion

    #region Constructor
    public Credentials(string username, string password, string role)
    {
        Username = Guard.Against.NullOrWhiteSpace(username, nameof(username));
        PasswordHash = Guard.Against.NullOrInvalidInput(
            password,
            nameof(password),
            input => Validation.Password.IsMatch(input)
        );
        Role = Guard.Against.NullOrWhiteSpace(role, nameof(role));
    }
    #endregion
}
