using Domain.Constants;

namespace Shared.Accounts;

public static class AccountDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class Detail : Index
    {
        public string Department { get; set; } = default!;
        public string Education { get; set; } = default!;
    }
    public class Mutate
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string? Password { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string Education { get; set; } = default!;

        
    }
}