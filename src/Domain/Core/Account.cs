﻿using Ardalis.GuardClauses;
using Domain.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Core;

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
        Firstname = Guard.Against.InvalidFormat(firstname, nameof(firstname), Validation.Name);
        Lastname = Guard.Against.InvalidFormat(lastname, nameof(lastname), Validation.Name);
        Email = Guard.Against.InvalidFormat(email, nameof(email), Validation.Email);
        Role = Guard.Against.EnumOutOfRange(role, nameof(role));
        PasswordHash = HashPassword(
            Guard.Against.InvalidFormat(password, nameof(password), Validation.Password)
        );
        IsActive = true;
        Education = Guard.Against.InvalidFormat(education, nameof(education), Validation.Education);
        Department = Guard.Against.InvalidFormat(
            department,
            nameof(department),
            Validation.Departement
        );
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
