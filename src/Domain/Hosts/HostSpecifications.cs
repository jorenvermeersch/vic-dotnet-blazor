using Domain.Hosts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

[NotMapped]
public class HostSpecifications : Specifications
{
    #region Properties
    public IList<VirtualisationFactor> VirtualisationFactors { get; set; } =
        new List<VirtualisationFactor>();

    public new int Processors => CalculateVirtualProcessors();
    #endregion

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

    #region Methods
    private int CalculateVirtualProcessors()
    {
        // Sum of: Cores * virtualisation factor.
        return VirtualisationFactors.Select(vf => vf.Processor.Cores * vf.Factor).Sum();
    }

    public void AddProccessor(Processor processor, int virtualisationFactor)
    {
        VirtualisationFactors.Add(new VirtualisationFactor(processor, virtualisationFactor));
    }

    public void RemoveProcessor(Processor processor, int virtualisationFactor)
    {
        VirtualisationFactor? item = VirtualisationFactors
            .ToList()
            .Find(vf => (vf.Processor == processor) && (vf.Factor == virtualisationFactor));

        if (item is not null)
        {
            VirtualisationFactors.Remove(item);
        }
    }
    #endregion
}
