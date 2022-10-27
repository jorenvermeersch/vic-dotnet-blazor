using Ardalis.GuardClauses;

namespace Domain.Domain;
[ToString]
public class Credentials
{
    #region Properties
    public string Username { get; set; }
    public string Password { get; set; }
    #endregion

    #region Constructor
    public Credentials(string username, string password)
    {
        Username = Guard.Against.NullOrWhiteSpace(username, nameof(username));
        Password = Guard.Against.InvalidFormat(password, nameof(password), "[^ ].{6,}");
    }
    #endregion
}
