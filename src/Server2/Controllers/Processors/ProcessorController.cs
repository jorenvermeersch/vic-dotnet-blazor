using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Hosts;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Processors;

[ApiController]
[Route("/api/processors/")]
[Authorize]
public class ProcessorController : ControllerBase
{
    private readonly IProcessorService processorService;

    public ProcessorController(IProcessorService processorService)
    {
        this.processorService = processorService;
    }

    [SwaggerOperation("Returns a list of processors")]
    [HttpGet]
    public async Task<ProcessorResponse.GetIndex> GetIndex()
    {
        ProcessorResponse.GetIndex response = await processorService.GetIndexAsync(new ProcessorRequest.GetIndex());
        return response;
    }
}



