using Microsoft.AspNetCore.Mvc;
using Shared.Hosts;

namespace Server.Controllers.Processors;

[ApiController]
[Route("/api/processors/")]
public class ProcessorController : ControllerBase
{
    private readonly IProcessorService processorService;

    public ProcessorController(IProcessorService processorService)
    {
        this.processorService = processorService;
    }
}



