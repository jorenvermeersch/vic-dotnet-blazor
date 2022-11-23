using Bogus;
using BogusStore.Fakers.Common;
using Domain.Administrators;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            f.PickRandom(educations)));
    }
}
