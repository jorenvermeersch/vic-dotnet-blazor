using Domain.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Administrators;

[ToString]
public class Account : Entity
{
    #region Properties
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public string Department { get; set; }
    public string Education { get; set; }
    #endregion

    #region Constructors
    public Account() { }

    public Account(
        string firstname,
        string lastname,
        string email,
        Role role,
        string password,
        string department,
        string education
    )
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Role = role;
        PasswordHash = HashPassword(password);
        IsActive = true;
        Department = department;
        Education = education;
    }
    #endregion

    #region Methods
    private string HashPassword(string password)
    {
        SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2")); // Convert byte to hexadecimal.
        }
        return builder.ToString();
    }
    #endregion
}
