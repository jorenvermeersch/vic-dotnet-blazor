using Ardalis.GuardClauses;

namespace Domain.Domain;

[ToString]
public class Credentials
{
    #region Properties
    public string Username { get; set; }
    public string Role { get; set; } // Chosen by administrator. No finite options.
    #endregion

    #region Constructor
    public Credentials(string username, string role)
    {
        Username = Guard.Against.NullOrWhiteSpace(username, nameof(username));
        Role = role; // TODO: Add validation.
    }
    #endregion
}
