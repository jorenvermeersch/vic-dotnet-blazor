namespace Shared.Ports;

public interface IPortService
{
    Task<IEnumerable<PortDto>> GetAllAsync();

}

