namespace Shared.Hosts;

public class ProcessorDto
{
    public string Name { get; set; } = default!;
    public int Cores { get; set; }
    public int Threads { get; set; }
}
