using Client.Extensions;
using Shared.Hosts;
using Shared.VirtualMachines;
using System.Net.Http.Json;

namespace Client.Hosts;

public class HostService : IHostService
{
    private readonly HttpClient client;
    public HostService(HttpClient client)
    {
        this.client = client;
    }
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

    public async Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<HostResponse.GetDetail>($"api/hosts/{request.HostId}");
        return response!;
    }
    
    public async Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<HostResponse.GetIndex>("api/hosts");
        return response!;
    }
}
