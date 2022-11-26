namespace Shared.Hosts;

public interface IHostService
{
    Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request);
    Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request);
    Task<HostResponse.Create> CreateAsync(HostRequest.Create request);
    Task<HostResponse.Edit> EditAsync(HostRequest.Edit request);
    Task DeleteAsync(HostRequest.Delete request);
}