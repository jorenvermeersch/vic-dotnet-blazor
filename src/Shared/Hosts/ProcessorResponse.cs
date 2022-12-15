namespace Shared.Hosts;

public static class ProcessorResponse
{
    public class GetIndex
    {
        public List<ProcessorDto>? Processors { get; set; } = default!;
        public int TotalAmount { get; set; } = default!;
    }

    public class GetDetail
    {
        public ProcessorDto? Processor { get; set; } = default!;
    }
}
