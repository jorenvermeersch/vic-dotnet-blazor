using Domain.Hosts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Hosts;

namespace Services.Processors;

public class ProcessorService : IProcessorService
{
    private readonly VicDbContext dbContext;
    private readonly DbSet<Processor> processors;

    public ProcessorService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
        processors = dbContext.Processors;
    }

    public async Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request)
    {
        ProcessorResponse.GetIndex response = new();

        var query = processors.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(p => p.Name.Contains(request.SearchTerm));
        }

        response.TotalAmount = query.Count();

        query = query.OrderBy(x => x.Name);
        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        response.Processors = await query.Select(p => new ProcessorDto
        {
            Id = p.Id,
            Name = p.Name,
            Cores = p.Cores,
            Threads = p.Threads,
        }).ToListAsync();

        return response;
    }
}

