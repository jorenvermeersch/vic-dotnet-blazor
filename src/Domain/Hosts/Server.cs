using Domain.Common;
using Domain.VirtualMachines;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Hosts;

[NotMapped]
public class Server : Host<VirtualMachine>
{
    #region Constructors
    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}
