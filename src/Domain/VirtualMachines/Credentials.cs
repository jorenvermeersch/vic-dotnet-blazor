namespace Domain.VirtualMachines;

public class Credentials : Entity
{
    #region Properties
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = default!;  // Chosen by administrator. Infinite options.
    #endregion

    #region Constructor
    private Credentials() { }

    public Credentials(string username, string password, string role)
    {
        Username = username;
        PasswordHash = password;
        Role = role;
    }
    #endregion
}
