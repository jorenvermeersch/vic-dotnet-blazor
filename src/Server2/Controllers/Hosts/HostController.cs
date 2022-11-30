using Client.VirtualMachines;
using Microsoft.AspNetCore.Mvc;
using Services.VirtualMachines;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Hosts;

[ApiController]
[Route("/api/hosts/")]
public class HostController : ControllerBase
{
    private readonly IHostService hostService;

    public HostController(IHostService hostService)
    {
        this.hostService = hostService;
    }

    [SwaggerOperation("Returns a list of ports")]
    [HttpGet]
    public async Task<HostResponse.GetIndex> GetIndex()
    {
        HostResponse.GetIndex response = await hostService.GetIndexAsync(new HostRequest.GetIndex());
        return response;
    }

    [SwaggerOperation("Returns a specific host")]
    [HttpGet("{HostId}")]
    public async Task<HostResponse.GetDetail> GetDetails([FromRoute] HostRequest.GetDetail request)
    {
        HostResponse.GetDetail response = await hostService.GetDetailAsync(request);
        return response;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] HostRequest.Create request)
    {
        HostResponse.Create response = await hostService.CreateAsync(request);
        return CreatedAtAction(nameof(CreateAsync), response.HostId);
    }

    [SwaggerOperation("Edits a virtual machine.")]
    [HttpPut("{hostId}")]
    public async Task<IActionResult> EditAsync([FromBody] HostDto.Mutate model, long hostId)
    {
        HostResponse.Edit response = await hostService.EditAsync(new HostRequest.Edit { Host = model, HostId = hostId });
        return CreatedAtAction(nameof(EditAsync), response.HostId);
    }

}



