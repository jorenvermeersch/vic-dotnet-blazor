using Microsoft.AspNetCore.Mvc;
using Shared.Ports;

namespace Server.Controllers.Ports;

[ApiController]
[Route("/api/ports/")]
public class PortController : ControllerBase
{
    private readonly IPortService portService;

    public PortController(IPortService portService)
    {
        this.portService = portService;
    }
}



