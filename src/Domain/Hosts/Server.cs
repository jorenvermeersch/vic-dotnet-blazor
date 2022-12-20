using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Hosts;

public class Server : Host<VirtualMachine>
{
    #region Properties
    public new List<ServerHistory> History =>
        base.History
            .Select(
                history =>
                    new ServerHistory()
                    {
                        Host = (Server)history.Host,
                        Specifications = history.Specifications,
                        SpecificationsUsed = history.SpecificationsUsed,
                    }
            )
            .ToList();

    #endregion

    #region Constructors
    private Server() { }

    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}
