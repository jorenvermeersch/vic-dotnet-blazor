using Shared.Hosts;

namespace Shared.Ports;

public interface IPortService
{
    Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request);
    Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request);
}

