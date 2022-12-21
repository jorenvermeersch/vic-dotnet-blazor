namespace Shared.Hosts;

public class ProcessorDto
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public int Cores { get; set; }
    public int Threads { get; set; }
}
