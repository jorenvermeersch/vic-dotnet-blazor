namespace Shared.Ports;

public static class PortResponse
{
    public class GetAll
    {
        public List<PortDto>? Ports { get; set; }
        public int TotalAmount { get; set; }
    }

}
