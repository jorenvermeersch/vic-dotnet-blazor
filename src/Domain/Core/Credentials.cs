﻿namespace Domain.Core;

[ToString]
public class Credentials
{
    #region Properties
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // Chosen by administrator. Infinite options.
    #endregion

    #region Constructor
    public Credentials(string username, string password, string role)
    {
        Username = username;
        PasswordHash = password;
        Role = role;
    }
    #endregion
}