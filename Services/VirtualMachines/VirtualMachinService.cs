using Shared.VirtualMachines;

namespace Services.VirtualMachines;

public class VirtualMachinService : IVirtualMachineService
{
    public VirtualMachinService()
    {

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

    public Task<VirtualMachineResponse.GetAllDetails> GetAllDetailsAsync(VirtualMachineRequest.GetAllDetails request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.GetIndex> GetUnfinishedAsync(VirtualMachineRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
