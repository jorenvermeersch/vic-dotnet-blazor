namespace Shared.Accounts;

public static class AccountRequest
{
    public class GetIndex
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
        public List<string>? Roles { get; set; }
    }
    public class GetDetail
    {
        public long AccountId { get; set; }
    }

    public class Delete
    {
        public long AccountId { get; set; }
    }

    public class Create
    {
        public AccountDto.Mutate Account { get; set; } = default!;
    }

    public class Edit
    {
        public long AccountId { get; set; }
        public AccountDto.Mutate Account { get; set; } = default!;
    }
}
