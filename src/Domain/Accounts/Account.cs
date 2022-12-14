using Domain.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Accounts;

public class Account : Entity
{
    #region Properties
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; }
    public string PasswordHash { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Education { get; set; } = default!;
    #endregion

    #region Constructors
    public Account() { } // TODO: Change to private constructor after correct database mapping.

    public Account(
        string firstname,
        string lastname,
        string email,
        Role role,
        string password,
        string department,
        string education,
        bool isActive
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
        IsActive = isActive;
    }
    #endregion

    #region Methods
    public string HashPassword(string password)
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
