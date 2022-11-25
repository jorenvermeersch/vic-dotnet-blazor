using Client.Extensions;
using Shared.Account;
using Shared.VirtualMachine;
using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly HttpClient authenticatedClient;
    public VirtualMachineService(HttpClient authenticatedClient)
    {
        this.authenticatedClient = authenticatedClient;
    }

    public Task<AccountDto.Details> Add(AccountDto.Create newAccount)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto.Details> GetDetailAsync(long AccountId)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>("api/virtual-machines");
        return response;
    }

    public Task<int> GetCount()
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines");
        return response;
    }

    public Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"api/virtual-machines/account/{request.ObjectId}");
        return response;
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(VirtualMachineRequest.GetByObjectId request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(VirtualMachineRequest.GetByObjectId request)
    {
        throw new NotImplementedException();
    }
}
