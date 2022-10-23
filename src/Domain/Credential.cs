namespace Domain;
public class Credential {
    #region Properties
    public string UserName { get; set; }
    public string Password { get; set; }
    #endregion
    #region Constructor
    public Credential(string userName, string password) {
        UserName = userName;
        Password = password;
    }
    #endregion
}
