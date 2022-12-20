using Domain.Hosts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Hosts;

namespace Services.Processors;

public class ProcessorService : IProcessorService
{
    private readonly VicDBContext _dbContext;
    private readonly DbSet<Processor> _processors;

    public ProcessorService(VicDBContext dbContext)
    {
        _dbContext= dbContext;
       // _processors = _dbContext.Processors;
    }
    public async Task<ProcessorResponse.GetDetail> GetDetailAsync(ProcessorRequest.GetDetail request)
    {
        ProcessorResponse.GetDetail response = new();

        Processor processor = await _processors.Where(p => p.Id == request.ProcessorId).AsNoTracking().SingleOrDefaultAsync();

        response.Processor = new ProcessorDto
        {
            Name = processor.Name,
            Cores = processor.Cores,
            Threads = processor.Threads,
        };

        return response;
    }

    public async Task<ProcessorResponse.GetIndex> GetIndexAsync(ProcessorRequest.GetIndex request)
    {
        ProcessorResponse.GetIndex response = new();

        var query = _processors.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(p=>p.Name== request.SearchTerm);
        }

        response.TotalAmount = query.Count();

        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        response.Processors = await query.Select(p => new ProcessorDto
        {
            Name = p.Name,
            Cores = p.Cores,
            Threads = p.Threads,
        }).ToListAsync();

        return response;
    }
}

