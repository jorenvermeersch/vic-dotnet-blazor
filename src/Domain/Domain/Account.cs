using System.Security.Principal;
using Domain.Constants;

namespace Domain.Domain;
public class Account
{
    #region Properties
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public string Department { get; set; }
    public string Opleiding { get; set; }
    #endregion
    #region Constructors
    public Account(string firstName, string lastName, Role role, string password, string department, string opleiding)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        PasswordHash = HashPassword(password);
        IsActive = true;
        Department = department;
        Opleiding = opleiding;
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
