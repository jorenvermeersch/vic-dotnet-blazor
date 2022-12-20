using Ardalis.GuardClauses;
using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Hosts;

public class Server : Host<VirtualMachine>
{
    #region Properties
    public new List<ServerHistory> History => (List<ServerHistory>)base.History;
    public List<ServerProcessor> Processors
    {
        get =>
            Specifications.VirtualisationFactors
                .Select(
                    entry =>
                        new ServerProcessor()
                        {
                            Host = this,
                            Processor = entry.Key,
                            VirtualisationFactor = entry.Value
                        }
                )
                .ToList();
        set
        {
            Guard.Against.Null(value, nameof(Processors));

            var result = value
                .Select(
                    serverProcessor =>
                        new KeyValuePair<Processor, int>(
                            serverProcessor.Processor,
                            serverProcessor.VirtualisationFactor
                        )
                )
                .ToList();

            Specifications.VirtualisationFactors = result;
        }
    }
    #endregion

    #region Constructors
    private Server() { }

    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}
