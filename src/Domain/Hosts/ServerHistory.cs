using Domain.VirtualMachines;

namespace Domain.Hosts;

public class ServerHistory : History<Server, VirtualMachine>
{
    private ServerHistory() { }

    public ServerHistory(Server server) : base(server) { }
}
