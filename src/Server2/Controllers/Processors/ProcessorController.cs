using Client.VirtualMachines;
using Microsoft.AspNetCore.Mvc;
using Shared.Hosts;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

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

    [SwaggerOperation("Returns a list of processors")]
    [HttpGet]
    public async Task<ProcessorResponse.GetIndex> GetIndex()
    {
        ProcessorResponse.GetIndex response = await processorService.GetIndexAsync(new ProcessorRequest.GetIndex());
        return response;
    }


    [SwaggerOperation("Returns a specific processor")]
    [HttpGet("{ProcessorId}")]
    public async Task<ProcessorResponse.GetDetail> GetDetails([FromRoute] ProcessorRequest.GetDetail request)
    {
        ProcessorResponse.GetDetail response = await processorService.GetDetailAsync(request);
        return response;
    }

}



