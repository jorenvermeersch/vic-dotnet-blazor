using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("/api/virtual-machines/")]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    [SwaggerOperation("Returns a list of virtual machines.")]
    [HttpGet]
    public async Task<VirtualMachineResponse.GetIndex> GetIndex()
    {
        VirtualMachineRequest.GetIndex request = new() { Offset = 50 };
        VirtualMachineResponse.GetIndex virtualMachineResponse = await virtualMachineService.GetIndexAsync(request);
        return virtualMachineResponse;
    }

    [SwaggerOperation("Returns a list of virtual machines.")]
    [HttpGet("{MachineId}")]
    public async Task<VirtualMachineResponse.GetDetail> GetDetail([FromRoute] VirtualMachineRequest.GetDetail request)
    {
        VirtualMachineResponse.GetDetail virtualMachineResponse = await virtualMachineService.GetDetailAsync(request);
        return virtualMachineResponse;
    }

    [SwaggerOperation("Creates a new virtual malchine.")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VirtualMachineDto.Mutate model)
    {
        VirtualMachineResponse.Create response = await virtualMachineService.CreateAsync(new VirtualMachineRequest.Create() { VirtualMachine = model });
        return CreatedAtAction(nameof(Create), response.MachineId);
    }

}
