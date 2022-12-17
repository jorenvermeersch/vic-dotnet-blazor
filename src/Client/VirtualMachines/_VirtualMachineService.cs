using Client.Extensions;
using Shared.VirtualMachines;
using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly HttpClient client;
    public VirtualMachineService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        var response = await client.PostAsJsonAsync($"api/virtual-machines", request);
        return await response.Content.ReadFromJsonAsync<VirtualMachineResponse.Create>();
    }

    public Task DeleteAsync(VirtualMachineRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResponse.GetAllDetails> GetAllDetailsAsync(VirtualMachineRequest.GetAllDetails request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<VirtualMachineResponse.GetAllDetails>($"api/virtual-machines/alldetails");
        return response!;
    }

    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<VirtualMachineResponse.GetDetail>($"api/virtual-machines/{request.MachineId}");
        return response!;
    }


    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await client.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines?{queryParameters}");
        return response!;
    }

    //public async Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    //{
    //    var queryParameters = request.GetQueryString();
    //    var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines/account/{request.ObjectId}");
    //    return response;
    //}
}
