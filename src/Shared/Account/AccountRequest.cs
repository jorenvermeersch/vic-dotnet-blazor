namespace Shared.Account;

public static class AccountRequest
{
    public class GetIndex
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
    }

    public class GetDetail
    {
        public int AccountId { get; set; }
    }

    public class Delete
    {
    }

    public class Create
    {
    }

    public class Edit
    {
    }
}
