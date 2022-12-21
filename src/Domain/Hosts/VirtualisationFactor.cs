namespace Domain.Hosts;

public class VirtualisationFactor : Entity
{
    public Processor Processor { get; set; } = default!;
    public int Factor { get; set; }

    private VirtualisationFactor() { }

    public VirtualisationFactor(Processor processor, int factor)
    {
        Processor = processor;
        Factor = factor;
    }
}
