using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Hosts;

public class Server : Host<VirtualMachine>
{
    #region Constructors
    private Server() { }

    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}
