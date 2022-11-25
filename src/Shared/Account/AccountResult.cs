namespace Shared.Account;

public static class AccountResult
{
    public class Index
    {
        public List<AccountDto.Index>? Accounts { get; set; }
        public int TotalAmount { get; set; }
    }

}
