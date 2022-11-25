namespace Shared.Host;

public class HostResult
{
    public class Index
    {
        public List<HostDto.Index>? Hosts { get; set; }
        public int TotalAmount { get; set; }
    }
}
