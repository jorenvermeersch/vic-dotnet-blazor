using Client.Extensions;
using Shared.Accounts;
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

    public Task<AccountDto.Detail> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto.Detail> GetDetailAsync(long AccountId)
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

    public Task<VirtualMachineDto.Detail> Add(VirtualMachineDto.Mutate newVM)
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
