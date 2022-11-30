using Shared.Hosts;

namespace Services.Processors;

public class ProcessorService : IProcessorService
{
    public Task<ProcessorResponse.GetDetail> GetDetailAsync(ProcessorRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}

