using BogusStore.Fakers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;

namespace Fakers.Credentials;

public class CredentialFaker : EntityFaker<Domain.Core.Credentials>
{
	public CredentialFaker()
	{
		CustomInstantiator(f => new Domain.Core.Credentials(
			username: f.Internet.UserName(),
			password: f.Internet.Password(),
			role: f.PickRandom(new[] { "Admin", "User", "Observer" })
        ));
	}
}
