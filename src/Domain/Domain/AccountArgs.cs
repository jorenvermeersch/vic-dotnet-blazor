using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain;

public class AccountArgs
{
    #region Properties
    public int Id { get;  set; }
    public string Firstname { get;  set; }
    public string Lastname { get;  set; }
    public string Email { get;  set; }
    public Role Role { get;  set; }
    public string Password { get;  set; }
    public bool IsActive { get;  set; }
    public string Department { get;  set; }
    public string Education { get;  set; }
    #endregion

    public AccountArgs() { }
}
