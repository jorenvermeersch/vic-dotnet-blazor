using Shared.Hosts;
using Domain.Hosts;
using Fakers.Processors;

namespace Services.Processors;

public class FakeProcessorService : IProcessorService
{
    private static readonly List<Processor> processors = new List<Processor>();

    static FakeProcessorService()
    {
        ProcessorFaker faker = new ProcessorFaker();
        processors = faker.UseSeed(1337).Generate(50);
    }

    public async Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request)
    {
        // FILTEREN OP NAAM

        var query = processors.AsQueryable();

        int totalAmount = query.Count();

        var items = query
           .Skip((request.Page - 1) * request.Amount)
           .Take(request.Amount)
           .OrderBy(x => x.Id)
           .Select(x => new ProcessorDto
           {
               Name = x.Name,
               Cores = x.Cores,
               Threads = x.Threads,
           }).ToList();

        var result = new ProcessorResponse.GetIndex
        {
            Processors = items,
            TotalAmount = totalAmount
        };

        return result;
    }

    public async Task<ProcessorResponse.GetDetail> GetDetailAsync(ProcessorRequest.GetDetail request)
    {
        ProcessorResponse.GetDetail response = new();

        response.Processor = processors.Where(x => x.Id == request.ProcessorId).Select(x => new ProcessorDto {
            Name = x.Name,
            Cores = x.Cores,
            Threads = x.Threads,
        }).SingleOrDefault();

        return response;
    }
}

