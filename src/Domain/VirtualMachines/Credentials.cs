namespace Domain.VirtualMachines;

[ToString]
public class Credentials : Entity
{
    #region Properties
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // Chosen by administrator. Infinite options.
    #endregion

    #region Constructor
    public Credentials() { }

    public Credentials(string username, string password, string role)
    {
        Username = username;
        PasswordHash = password;
        Role = role;
    }
    #endregion
}
