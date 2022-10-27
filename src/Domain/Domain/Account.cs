using Ardalis.GuardClauses;
using Domain.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Domain;
[ToString]
public class Account : Entity
{
    #region Properties
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public Role Role { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    public string Department { get; private set; }
    public string Education { get; private set; }
    #endregion

    #region Constructors
    public Account(string firstname, string lastname, string email, Role role, string password, string department, string education)
    {
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(firstname), "[^0-9\\W]+");
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(lastname), "[^0-9\\W]+");
        Email = Guard.Against.InvalidFormat(email, nameof(email), "^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-z]+)$");
        Role = role;
        PasswordHash = HashPassword(Guard.Against.InvalidFormat(password, nameof(password), "[^ ].{6,}"));
        IsActive = true;
        Education = Guard.Against.InvalidFormat(education, nameof(education), "^.{0}$|^[a-zA-Z]+[ a-zA-Z]*");
        Department = Guard.Against.InvalidFormat(department, nameof(department), "^[a-zA-Z]+[ a-zA-Z]*");
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
            builder.Append(bytes[i].ToString("x2")); //convert byte to hexadecimal
        }
        return builder.ToString();
    }

    #endregion
}
