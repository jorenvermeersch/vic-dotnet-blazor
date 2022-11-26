using Microsoft.AspNetCore.Mvc;
using Shared.Hosts;

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
}



