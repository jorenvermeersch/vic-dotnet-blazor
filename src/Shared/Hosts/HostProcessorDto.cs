namespace Shared.Hosts;

public class HostProcessorDto
{
    public ProcessorDto Processor { get; set; } = default!;
    public int VirtualisationFactor { get; set; }
}
