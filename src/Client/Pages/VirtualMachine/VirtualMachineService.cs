using Client.Extensions;
using Shared.Account;
using Shared.VirtualMachine;
using System.Net.Http.Json;

namespace Client.Pages.VirtualMachine
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly HttpClient authenticatedClient;
        private const string endpoint = "api/virtual-machines";
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
            var response = await authenticatedClient.GetFromJsonAsync<VirtualMachineResponse.GetIndex>($"{endpoint}/");
            return response!;
        }

        Task<VirtualMachineDto.Details> IVirtualMachineService.GetDetailAsync(long virtualMachineId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(long hostId, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByAccountId(long accountId, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.GetIndex request)
        {
            throw new NotImplementedException();
        }
    }
}
