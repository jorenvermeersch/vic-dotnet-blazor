using Ardalis.GuardClauses;

namespace Domain.Domain;
public class Credential
{
    #region Properties
    public string Username { get; set; }
    public string Password { get; set; }
    #endregion
    #region Constructor
    public Credential(string username, string password)
    {
        Username = Guard.Against.NullOrWhiteSpace(username, nameof(username));
        Password = Guard.Against.InvalidFormat(password, nameof(password), "[a-zA-Z0-9.@&?+]{6,}");
    }
    #endregion
}
