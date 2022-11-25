namespace Shared.Host;

public class HostSpecificationsDto
{
    public List<KeyValuePair<ProcessorDto, int>> Processors { get; set; } = new();
    public int Memory { get; set; }
    public int Storage { get; set; }
}
