namespace Shared.Port;

public interface IPortService
{
    Task<IEnumerable<PortDto>> GetAllAsync();

}

