﻿namespace Shared.VirtualMachine;

public class CredentialDto
{
    public string Username { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
}