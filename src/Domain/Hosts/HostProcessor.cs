using Domain.Common;

namespace Domain.Hosts;

public class HostProcessor<T, U> : Entity
    where T : Host<U>
    where U : Machine
{
    #region Fields
    public long HostId { get; set; }
    public long ProcessorId { get; set; }
    public virtual T Host { get; set; } = default!;
    public virtual Processor Processor { get; set; } = default!;
    public int VirtualisationFactor { get; set; } = default!;
    #endregion

    #region Constructors
    protected HostProcessor() { }

    public HostProcessor(T host, Processor processor, int virtualisationFactor)
    {
        Host = host;
        Processor = processor;
        VirtualisationFactor = virtualisationFactor;
    }
    #endregion
}
