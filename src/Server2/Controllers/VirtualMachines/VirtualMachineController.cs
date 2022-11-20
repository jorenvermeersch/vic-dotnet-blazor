using Microsoft.AspNetCore.Mvc;
using Shared.Account;
using Shared.VirtualMachine;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("/api/virtual-machines")]
public class VirtualMachineController
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    [SwaggerOperation("Returns a list of virtual machines")]
    [HttpGet]
    public async Task<VirtualMachineResponse.GetIndex> GetIndex()
    {
        VirtualMachineRequest.GetIndex request = new() { Offset = 50 };
        VirtualMachineResponse.GetIndex virtualMachineResponse = await virtualMachineService.GetIndexAsync(request);
        return virtualMachineResponse;
    }
}
