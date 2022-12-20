using Domain.Common;

namespace Domain.Hosts;

public class HostProcessor<T, U> : Entity
    where T : Host<U>
    where U : Machine
{
	#region Fields
    public T Host { get; set; } = default!;
    public Processor Processor { get; set; } = default!;
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
