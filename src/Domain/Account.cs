using System.Security.Principal;

namespace Domain;
public class Account {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public string Department { get; set; }
    public string Opleiding { get; set; }
    public Account(string firstName, string lastName, Role role, string password,string department, string opleiding) {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        PasswordHash = HashPassword(password);
        IsActive = false;
        Department = department;
        Opleiding = opleiding;
    }
    private string HashPassword(string password) {
        throw new NotImplementedException();
    }
    private string ChangePassword(string newPassword) {
        throw new NotImplementedException();
    }
}
