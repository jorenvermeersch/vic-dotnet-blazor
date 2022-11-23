using BogusStore.Fakers.Common;

namespace Fakers.Credentials;

public class CredentialFaker : EntityFaker<Domain.VirtualMachines.Credentials>
{
    public CredentialFaker()
    {
        CustomInstantiator(f => new Domain.VirtualMachines.Credentials(
            username: f.Internet.UserName(),
            password: f.Internet.Password(),
            role: f.PickRandom(new[] { "Admin", "User", "Observer" })
        ));
    }
}
