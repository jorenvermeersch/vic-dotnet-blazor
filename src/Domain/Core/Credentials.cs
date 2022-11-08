using Ardalis.GuardClauses;

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
    public Credentials(string username, string passwordHash, string role)
    {
        Username = Guard.Against.NullOrWhiteSpace(username, nameof(Username));
        PasswordHash = Guard.Against.NullOrWhiteSpace(passwordHash, nameof(PasswordHash));
        Role = Guard.Against.NullOrWhiteSpace(role, nameof(Role));
    }
    #endregion
}
