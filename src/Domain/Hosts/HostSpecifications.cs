using Domain.Hosts;

namespace Domain.Common;

public class HostSpecifications : Specifications
{
    #region Properties
    public Dictionary<Processor, int> VirtualisationFactors { get; set; } = new(); // Key : Value => Processor : virtualisation factor.
    public new int Processors =>
        VirtualisationFactors.Select(pair => pair.Key.Cores * pair.Value).Sum(); // Sum of: Cores * virtualisation factor.
    #endregion

    #region Constructors
    public HostSpecifications(Dictionary<Processor, int> processors, int storage, int memory)
        : base(memory, storage)
    {
        VirtualisationFactors = processors;
        Storage = storage;
        Memory = memory;
    }
    #endregion
}
