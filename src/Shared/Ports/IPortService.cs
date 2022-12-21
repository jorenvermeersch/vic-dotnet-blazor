namespace Shared.Ports;

public interface IPortService
{
    Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request);
}

