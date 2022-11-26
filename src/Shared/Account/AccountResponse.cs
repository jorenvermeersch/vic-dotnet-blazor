namespace Shared.Account;

public static class AccountResponse
{
    public class GetIndex
    {
        public List<AccountDto.Index>? Accounts { get; set; }
        public int TotalAmount { get; set; }
    }

    public class GetDetail
    {
        public AccountDto.Detail Account { get; set; } = default!;
    }

    public class Delete
    {
    }

    public class Create
    {
        public long AccountId { get; set; }
    }

    public class Edit
    {
        public long AccountId { get; set; }
    }

}
