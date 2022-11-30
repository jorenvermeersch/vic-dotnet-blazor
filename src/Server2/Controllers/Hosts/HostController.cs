using Microsoft.AspNetCore.Mvc;
using Shared.Hosts;
using Shared.Ports;
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

}



