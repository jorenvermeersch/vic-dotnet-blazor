namespace Shared.Hosts;

public interface IProcessorService
{
    Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request);
    Task<ProcessorResponse.GetDetail> GetDetailAsync(ProcessorRequest.GetDetail request);
}
