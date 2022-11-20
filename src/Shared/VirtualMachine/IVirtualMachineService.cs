using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public interface IVirtualMachineService
{

    Task<VirtualMachineResponse.GetIndex> GetIndexAsync(/*int offset, int amount*/ VirtualMachineRequest.GetIndex request);
    Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId, int offset);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(long hostId, int offset);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByAccountId(long accountId, int offset);
    Task<int> GetCount();

    Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM);

    Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(/*int offset*/ VirtualMachineRequest.GetIndex request);

}
