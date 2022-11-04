
namespace Shared.Host;

public interface IHostService
{
    Task<IEnumerable<HostDto>> GetIndexAsync(int offset);
    Task<HostDto> GetDetailAsync(long hostId);
    Task<int> GetCount();
}