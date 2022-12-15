using Shared.Accounts;

namespace Client.Extensions;

public static class AccountExtensions
{
    private static string route = "account";

    public static string GetFullName(this AccountDto.Index account)
    {
        return $"{account.Firstname} {account.Lastname}";
    }

    public static string GetDetailUrl(this AccountDto.Index account)
    {
        return $"{route}/{account.Id}";
    }
}
