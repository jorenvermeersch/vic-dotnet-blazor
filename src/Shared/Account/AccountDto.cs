namespace Shared.Account;

public static class AccountDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Details
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string Department { get; set; }
        public string Education { get; set; }
    }

}