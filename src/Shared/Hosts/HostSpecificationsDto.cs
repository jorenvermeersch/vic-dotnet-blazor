namespace Shared.Hosts;

public class HostSpecificationsDto
{
    public List<KeyValuePair<ProcessorDto, int>> Processors { get; set; } = new();
    public int VirtualProcessors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
}
