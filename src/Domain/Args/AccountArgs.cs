using Domain.Constants;

namespace Domain.Args;

public class AccountArgs
{
    #region Properties
    public long Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string Department { get; set; }
    public string Education { get; set; }
    #endregion
}
