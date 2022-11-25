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

    public Task<AccountDto.Details> Add(AccountDto.Mutate newAccount)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto.Details> GetDetailAsync(long AccountId)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResult.GetIndex> GetIndexAsync(VirtualMachineRequest.Index request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResult.GetIndex>("api/virtual-machines");
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

    public async Task<VirtualMachineResult.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.Index request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResult.GetIndex>($"api/virtual-machines");
        return response;
    }

    public Task<VirtualMachineResult.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineResult.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResult.GetIndex>($"api/virtual-machines/account/{request.ObjectId}");
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
