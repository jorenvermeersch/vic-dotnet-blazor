namespace Shared.Host;

public class HostRequest
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
        public long HostId { get; set; }
    }

    public class Delete
    {
        public long HostId { get; set; }
    }

    public class Create
    {
        public HostDto.Mutate Host { get; set; } = default!;
    }

    public class Edit
    {
        public long HostId { get; set; }
        public HostDto.Mutate Host { get; set; } = default!;
    }
}
