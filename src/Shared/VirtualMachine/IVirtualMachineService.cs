using Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public interface IVirtualMachineService
{

    Task<VirtualMachineResponse.GetIndex> GetIndexAsync(/*int offset, int amount*/ VirtualMachineRequest.GetIndex request);
    Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(VirtualMachineRequest.GetByObjectId request);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(VirtualMachineRequest.GetByObjectId request);
    //Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByAccountId(long accountId, int offset);
    Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request);
    Task<int> GetCount();

    Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM);

    Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(/*int offset*/ VirtualMachineRequest.GetIndex request);

}
