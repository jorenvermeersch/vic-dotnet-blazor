namespace Shared.Host;

public interface IHostService
{
    Task<HostResult.Index> GetIndexAsync(HostRequest.Index request);
    Task<HostDto.Detail> GetDetailAsync(long hostId);
    Task<long> CreateAsync(HostDto.Mutate model);
    Task EditAsync(long hostId, HostDto.Mutate model);
    Task DeleteAsync(long hostId);
}