namespace Shared.Ports;

public interface IPortService
{
    Task<PortResponse.GetAll> GetAllAsync();
}

