using Shared.Hosts;

namespace Shared.Ports;

public static class PortResponse
{
    public class GetAll
    {
        public List<PortDto>? Ports { get; set; } = default!;
        public int TotalAmount { get; set; } = default!;
    }

    public class GetDetail
    {
        public PortDto? Port { get; set; } = default!;
    }

}
