
namespace Shared.Host;

public interface IHostService
{
    Task<IEnumerable<HostDto.Index>> GetIndexAsync(int offset);
    Task<HostDto.Details> GetDetailAsync(long hostId);
    Task<int> GetCount();
    Task<HostDto.Details> Add(HostDto.Details newHost);
}