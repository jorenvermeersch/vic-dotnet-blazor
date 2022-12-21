namespace Shared.Hosts;

public interface IProcessorService
{
    Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request);
}
