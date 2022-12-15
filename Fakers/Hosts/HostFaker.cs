using BogusStore.Fakers.Common;
using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;

namespace Fakers.Hosts;

public class HostFaker : EntityFaker<Server>
{
    public HostFaker(List<HostSpecifications> hostSpecifications, List<VirtualMachine> virtualMachines)
    {
        CustomInstantiator(f => new Server(
            name: f.Internet.UserName(),
            resources: f.PickRandom(hostSpecifications),
            virtualMachines: virtualMachines.Count() == 0 ? null : Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(virtualMachines)).ToHashSet()
        ));;
    }
}
