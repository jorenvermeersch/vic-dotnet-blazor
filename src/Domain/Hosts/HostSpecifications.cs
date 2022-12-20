using Ardalis.GuardClauses;
using Domain.Hosts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

[NotMapped]
public class HostSpecifications : Specifications
{
    #region Properties
    public IList<KeyValuePair<Processor, int>> VirtualisationFactors { get; set; } =
        new List<KeyValuePair<Processor, int>>(); // Key : Value => Processor : Virtualisation factor.
    public new int Processors
    {
        get => CalculateVirtualProcessors();
        private set => base.Processors = value;
    }

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
        Processors = CalculateVirtualProcessors();
        Storage = storage;
        Memory = memory;
    }
    #endregion

    #region Methods
    private int CalculateVirtualProcessors()
    {
        // Sum of: Cores * virtualisation factor.
        return VirtualisationFactors.Select(pair => pair.Key.Cores * pair.Value).Sum();
    }

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
