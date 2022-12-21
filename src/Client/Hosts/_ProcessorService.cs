using Client.Extensions;
using Shared.Hosts;
using System.Net.Http.Json;

namespace Client.Hosts;

public class ProcessorService : IProcessorService
{
    private readonly HttpClient client;
    public ProcessorService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request)
    {
        var queryString = request.GetQueryString();
        var response = await client.GetFromJsonAsync<ProcessorResponse.GetIndex>($"api/processors?{queryString}");
        return response!;
    }
}
