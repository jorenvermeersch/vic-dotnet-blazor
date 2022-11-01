using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public interface IVirtualMachineService
{

    Task<IEnumerable<VirtualMachineDto.Index>> GetIndexAsync();
    Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId);
    Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId);
}
