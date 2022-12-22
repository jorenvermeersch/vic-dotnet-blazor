using Domain.Hosts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

[NotMapped]
public class HostSpecifications : Specifications
{
    // VirtualisationFactors are now stored in Host itself. Did not have enough time to refactor this properly. 
    public IList<VirtualisationFactor> VirtualisationFactors { get; set; } =
        new List<VirtualisationFactor>();

    #region Constructors
    private HostSpecifications() { }

    public HostSpecifications(IList<VirtualisationFactor> processors, int storage, int memory)
        : base(memory, storage)
    {
        VirtualisationFactors = processors;
        Storage = storage;
        Memory = memory;
    }
    #endregion
}
