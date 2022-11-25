using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachine;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("/api/virtual-machines/")]
public class VirtualMachineController
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    /// <summary>
    /// Returns a list of virtual machines
    /// </summary>
    /// <returns></returns>
    [SwaggerOperation("Returns a list of virtual machines")]
    [HttpGet]
    public async Task<VirtualMachineResult.GetIndex> GetIndex()
    {
        VirtualMachineRequest.Index request = new() { Offset = 50 };
        VirtualMachineResult.GetIndex virtualMachineResponse = await virtualMachineService.GetIndexAsync(request);
        return virtualMachineResponse;
    }


    [SwaggerOperation("Returns a list of virtual machines that belongs to a specific account [ObjectId is the Id of the account]")]
    [HttpGet("account/{ObjectId}")]
    public async Task<VirtualMachineResult.GetIndex> GetVirtualMachinesByAccountId([FromRoute] VirtualMachineRequest.GetByObjectId request)
    {
        VirtualMachineResult.GetIndex virtualMachineResponse = await virtualMachineService.GetVirtualMachinesByAccountId(request);
        return virtualMachineResponse;
    }
}
