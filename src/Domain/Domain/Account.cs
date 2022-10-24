using System.Security.Principal;
using Ardalis.GuardClauses;
using Domain.Constants;

namespace Domain.Domain;
public class Account
{
    #region Properties
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Role Role { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public string Department { get; set; }
    public string Education { get; set; }
    #endregion
    #region Constructors
    public Account(string firstname, string lastname, Role role, string password, string department, string education)
    {
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(firstname), "[a-zA-Z]+");
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(lastname), "[a-zA-Z]+");
        Role = role;
        PasswordHash = HashPassword(password);
        IsActive = true;
        Education = Guard.Against.InvalidFormat(education, nameof(education), "^.{0}$|^[a-zA-Z]+[ a-zA-Z]*");
        Department = Guard.Against.InvalidFormat(department, nameof(department), "^[a-zA-Z]+[ a-zA-Z]*");
    }
    #endregion
    #region Methods
    private string HashPassword(string password)
    {
        throw new NotImplementedException();
    }
    private string ChangePassword(string newPassword)
    {
        throw new NotImplementedException();
    }
    #endregion
}
