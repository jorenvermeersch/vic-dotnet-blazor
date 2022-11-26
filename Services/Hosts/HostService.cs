using Shared.Hosts;

namespace Services.Hosts;

public class HostService : IHostService
{
    public Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(HostRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<HostResponse.Edit> EditAsync(HostRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
