using Client.Extensions;
using Shared.Ports;
using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class PortService : IPortService
{
    private readonly HttpClient client;
    public PortService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<PortResponse.GetAll>($"api/ports?{queryParameters}");
        return response!;
    }

    public Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }
}
