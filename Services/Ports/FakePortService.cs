using Shared.Ports;

namespace Services.Ports;

public class FakePortService : IPortService
{
    public readonly List<PortDto> ports = new();

    public FakePortService()
    {
        ports.Add(new PortDto()
        {
            Service = "HTTPS",
            Number = 443
        });
        ports.Add(new PortDto()
        {
            Service = "HTTP",
            Number = 80
        });
        ports.Add(new PortDto()
        {
            Service = "SSH",
            Number = 22
        });
    }

    public Task<IEnumerable<PortDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
