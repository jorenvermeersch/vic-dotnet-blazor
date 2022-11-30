using Client.Extensions;
using Shared.VirtualMachines;
using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly HttpClient authenticatedClient;
    public VirtualMachineService(HttpClient authenticatedClient)
    {
        this.authenticatedClient = authenticatedClient;
    }

    public Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(VirtualMachineRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetDetail>($"api/virtual-machines/{request.MachineId}");
        return response!;
    }

    public Task<VirtualMachineResponse.GetIndex> GetUnfinishedAsync(VirtualMachineRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }


    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>("api/virtual-machines");
        return response!;
    }

    //public async Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.GetIndex request)
    //{
    //    var queryParameters = request.GetQueryString();
    //    var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines");
    //    return response;
    //}

    //public async Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    //{
    //    var queryParameters = request.GetQueryString();
    //    var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines/account/{request.ObjectId}");
    //    return response;
    //}
}
