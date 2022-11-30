using BogusStore.Fakers.Common;
using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Specifications;
using Fakers.VirtualMachines;

namespace Fakers.Hosts;

public class HostFaker : EntityFaker<Server>
{
    public HostFaker()
    {
        CustomInstantiator(f => new Server(
            name: f.Internet.UserName(),
            resources: new HostSpecificationsFaker(), //todo
            virtualMachines: (ISet<VirtualMachine>) new VirtualMachineFaker().Generate(3).ToList()
        ));
    }
}
