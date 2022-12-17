using Ardalis.GuardClauses;
using Domain.Hosts;

namespace Domain.Common;

public class HostSpecifications : Specifications
{
    #region Properties
    public IList<KeyValuePair<Processor, int>> VirtualisationFactors { get; set; } =
        new List<KeyValuePair<Processor, int>>(); // Key : Value => Processor : Virtualisation factor.
    public new int Processors =>
        VirtualisationFactors.Select(pair => pair.Key.Cores * pair.Value).Sum(); // Sum of: Cores * virtualisation factor.
    #endregion

    #region Constructors
    private HostSpecifications() { }

    public HostSpecifications(
        IList<KeyValuePair<Processor, int>> processors,
        int storage,
        int memory
    ) : base(memory, storage)
    {
        VirtualisationFactors = processors;
        Storage = storage;
        Memory = memory;
    }
    #endregion

    #region Methods
    public void AddProccessor(Processor processor, int virtualisationFactor)
    {
        Guard.Against.NegativeOrZero(virtualisationFactor, nameof(virtualisationFactor));
        VirtualisationFactors.Add(
            new KeyValuePair<Processor, int>(processor, virtualisationFactor)
        );
    }

    public void RemoveProcessor(Processor processor, int virtualisationFactor)
    {
        Guard.Against.NegativeOrZero(virtualisationFactor, nameof(virtualisationFactor));
        KeyValuePair<Processor, int> item = VirtualisationFactors
            .Where(item => item.Key.Id == processor.Id && item.Value == virtualisationFactor)
            .FirstOrDefault();
        VirtualisationFactors.Remove(item);
    }
    #endregion
}
