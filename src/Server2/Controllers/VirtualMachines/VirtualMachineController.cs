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
    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync([FromQuery] VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex virtualMachineResponse = await virtualMachineService.GetIndexAsync(request);
        return virtualMachineResponse;
    }

    [SwaggerOperation("Returns a specific virtual machine.")]
    [HttpGet("{MachineId}")]
    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync([FromRoute] VirtualMachineRequest.GetDetail request)
    {
        VirtualMachineResponse.GetDetail virtualMachineResponse = await virtualMachineService.GetDetailAsync(request);
        return virtualMachineResponse;
    }

    [SwaggerOperation("Creates a new virtual malchine.")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] VirtualMachineDto.Mutate model)
    {
        VirtualMachineResponse.Create response = await virtualMachineService.CreateAsync(new VirtualMachineRequest.Create() { VirtualMachine = model });
        return CreatedAtAction(nameof(CreateAsync), response.MachineId);
    }

    [SwaggerOperation("Edits a virtual machine.")]
    [HttpPut("{machineId}")]
    public async Task<IActionResult> EditAsync([FromBody] VirtualMachineDto.Mutate model, long machineId)
    {
        VirtualMachineResponse.Edit response = await virtualMachineService.EditAsync(new VirtualMachineRequest.Edit() { MachineId = machineId, VirtualMachine = model });
        return Accepted(nameof(EditAsync), response.MachineId);
    }

    [SwaggerOperation("Deletes a virtual machine")]
    [HttpDelete("{machineId}")]
    public async Task<IActionResult> Delete(int machineId)
    {
        await virtualMachineService.DeleteAsync(new VirtualMachineRequest.Delete { MachineId = machineId });
        return NoContent();
    }

}
