namespace Shared.Host;

public class HostResponse
{
    public class GetIndex
    {
        public List<HostDto.Index>? Hosts { get; set; }
        public int TotalAmount { get; set; }
    }

    public class GetDetail
    {
        public HostDto.Detail Host { get; set; } = default!;
    }

    public class Delete
    {
    }

    public class Create
    {
        public long HostId { get; set; }
    }

    public class Edit
    {
        public long HostId { get; set; }
    }
}
