using Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public interface IVirtualMachineService
{

    Task<VirtualMachineResult.GetIndex> GetIndexAsync(/*int offset, int amount*/ VirtualMachineRequest.Index request);
    Task<VirtualMachineResult.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(VirtualMachineRequest.GetByObjectId request);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(VirtualMachineRequest.GetByObjectId request);
    //Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByAccountId(long accountId, int offset);
    Task<VirtualMachineResult.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request);
    Task<int> GetCount();

    Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM);

    Task<VirtualMachineResult.GetIndex> GetAllUnfinishedVirtualMachines(/*int offset*/ VirtualMachineRequest.Index request);

}
