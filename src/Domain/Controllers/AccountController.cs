

using Domain.Domain;

namespace Domain.Controllers;

public class AccountController
{
    private VIC _vic = VIC.Instance;

    public ISet<Account> GetAllAccounts()
    {
        return _vic.AccountRepo.Accounts;
    }

    public Account GetAccountByEmail(string email)
    {
        return _vic.AccountRepo.GetAccountByEmail(email);
    }

    public void CreateAccount(AccountArgs args)
    {
        _vic.CreateAccount(args);
    }
}
