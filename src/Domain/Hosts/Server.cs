using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Hosts;

public class Server : Host<VirtualMachine>
{
    #region Properties
    public new List<ServerHistory> History => (List<ServerHistory>)base.History;
    public List<ServerProcessor> Processors { get; set; } = default!;
    #endregion

    #region Constructors
    private Server() { }

    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}
