using Domain.Domain;

namespace Domain.Repositories;

public class AccountRepo
{
    private readonly ISet<Account> _accounts = new HashSet<Account>();
    public ISet<Account> Accounts => _accounts;

    public AccountRepo()
    {
        SeedData();
    }

    public Account GetAccountByEmail(string email)
    {
        return _accounts.First(a=>a.Email == email);
    }


    private void SeedData()
    {
        _accounts.Add(new Account("Jane", "Doe", "jane.doe@hotmail.com", Constants.Role.Admin, "Passwoord123", "DIT", "Toegepaste Informatica"));
        _accounts.Add(new Account("John", "Doe", "john.doe@hotmail.com", Constants.Role.Master, "Passwoord123", "DIT", ""));
        _accounts.Add(new Account("Jan", "Janssens", "jan.janssens@hotmail.com", Constants.Role.Observer, "Passwoord123", "DIT", "Toegepaste Informatica"));
    }

    internal void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
}
