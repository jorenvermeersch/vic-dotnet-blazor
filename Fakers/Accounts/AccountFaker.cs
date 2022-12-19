using BogusStore.Fakers.Common;
using Domain.Accounts;
using Domain.Constants;

namespace Fakers.Accounts;

public class AccountFaker : EntityFaker<Account>
{
    public AccountFaker(string locale = "nl")
    {
        var departments = new[] { "DIT", "DBT" };
        var educations = new[] { "", "Toegepaste Informatica", "Elektro-mechanica" };
        CustomInstantiator(f => new Account(f.Name.FirstName(),
            f.Name.LastName(),
            f.Internet.Email(),
            f.PickRandom(Enum.GetValues(typeof(Role)).Cast<Role>().ToList()),
            f.Internet.Password(),
            f.PickRandom(departments),
            f.PickRandom(educations),
            true
            )
        );
    }
}
