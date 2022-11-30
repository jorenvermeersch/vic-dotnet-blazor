using BogusStore.Fakers.Common;
using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Specifications;
using Fakers.VirtualMachines;

namespace Fakers.Hosts;

public class HostFaker : EntityFaker<Server>
{
    private readonly List<HostSpecifications> hostSpecifications = new List<HostSpecifications>();
    public HostFaker()
    {
        hostSpecifications = new HostSpecificationsFaker().UseSeed(1337).Generate(50);

        CustomInstantiator(f => new Server(
            name: f.Internet.UserName(),
            resources: f.PickRandom(hostSpecifications),
            virtualMachines: (ISet<VirtualMachine>) new VirtualMachineFaker().Generate(3).ToList()
        ));
    }
}
