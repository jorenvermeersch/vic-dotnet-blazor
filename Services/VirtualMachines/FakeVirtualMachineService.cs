using Domain.VirtualMachines;
using Fakers.VirtualMachines;
using Shared.VirtualMachines;

namespace Service.VirtualMachines;

public class FakeVirtualMachineService : IVirtualMachineService
{
    public readonly List<VirtualMachine> machines = new();
    public FakeVirtualMachineService()
    {
        VirtualMachineFaker virtualMachineFaker = new();
        machines = virtualMachineFaker.UseSeed(1337).Generate(10);
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
