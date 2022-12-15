namespace Shared.Hosts;

public static class ProcessorRequest
{
    public class GetIndex
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
    }

    public class GetDetail
    {
        public long ProcessorId { get; set; }
    }
}
