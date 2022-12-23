using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("/api/virtual-machines/")]
[Authorize]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    //api/virtual-machines/alldetails

    [SwaggerOperation("Returns a list of virtual machines with details.")]
    [HttpGet("alldetails")]
    public async Task<VirtualMachineResponse.GetAllDetails> GetIndexAsync([FromQuery] VirtualMachineRequest.GetAllDetails request)
    {
        VirtualMachineResponse.GetAllDetails virtualMachineResponse = await virtualMachineService.GetAllDetailsAsync(new VirtualMachineRequest.GetAllDetails());
        return virtualMachineResponse;
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
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> CreateAsync([FromBody] VirtualMachineRequest.Create request)
    {
        VirtualMachineResponse.Create response = await virtualMachineService.CreateAsync(request);
        return CreatedAtAction(nameof(CreateAsync), response);
    }

    [SwaggerOperation("Edits a virtual machine.")]
    [HttpPut]
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> EditAsync(VirtualMachineRequest.Edit request)
    {
        VirtualMachineResponse.Edit response = await virtualMachineService.EditAsync(request);
        return Accepted(nameof(EditAsync), response);
    }

    [SwaggerOperation("Deletes a virtual machine")]
    [HttpDelete("{machineId}")]
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> Delete(VirtualMachineRequest.Delete request)
    {
        await virtualMachineService.DeleteAsync(request);
        return NoContent();
    }

}
