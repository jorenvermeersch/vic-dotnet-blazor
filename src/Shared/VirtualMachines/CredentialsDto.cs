namespace Shared.VirtualMachines;

public class CredentialsDto
{
    public string Username { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}