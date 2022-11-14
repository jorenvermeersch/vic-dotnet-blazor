using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public interface IVirtualMachineService
{

    Task<IEnumerable<VirtualMachineDto.Index>> GetIndexAsync(int offset, int amount);
    Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId);
    Task<int> GetCount();

    Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Details newVM);

    Task<IEnumerable<VirtualMachineDto.Index>> GetAllUnfinishedVirtualMachines(int offset);
}
