using Domain.Domain;

namespace Domain.Repositories;

public class AccountRepository : EntityRepository<Account>
{
    public AccountRepository() : base()
    {
        SeedData();
    }

    public Account GetAccountByEmail(string email)
    {
        return Entities.First(a => a.Email == email);
    }


    private void SeedData()
    {
        Entities.Add(new Account("Jane", "Doe", "jane.doe@hotmail.com", Constants.Role.Admin, "Passwoord123", "DIT", "Toegepaste Informatica"));
        Entities.Add(new Account("John", "Doe", "john.doe@hotmail.com", Constants.Role.Master, "Passwoord123", "DIT", ""));
        Entities.Add(new Account("Jan", "Janssens", "jan.janssens@hotmail.com", Constants.Role.Observer, "Passwoord123", "DIT", "Toegepaste Informatica"));
    }
}
