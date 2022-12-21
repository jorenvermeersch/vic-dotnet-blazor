using Domain.Common;
using Domain.Exceptions;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Hosts;
using Shared.VirtualMachines;

namespace Services.Hosts;

public class HostService : IHostService
{

    private readonly VicDbContext dbContext;
    private readonly DbSet<Server> hosts;

    public HostService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
        hosts = dbContext.Hosts;
    }

    private IQueryable<Server> GetHostById(long id)
    {
        return hosts
               .AsNoTracking()
               .Where(p => p.Id == id);
    }

    public async Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        HostResponse.Create response = new();

        HostDto.Mutate model = request.Host;

        // Fetch and format processors. 
        List<VirtualisationFactor> processors = new();
        foreach (var entry in model.Specifications.Processors)
        {
            Processor? processor = await dbContext.Processors.Where(p => p.Id == entry.Key.Id).SingleOrDefaultAsync();
            if (processor is null) throw new EntityNotFoundException(nameof(Processor), entry.Key.Id);
            processors.Add(new VirtualisationFactor(processor, entry.Value));
        }

        var host = new Server(
                model.Name,
                new HostSpecifications(
                processors,
                model.Specifications.Storage,
                model.Specifications.Memory
            ), new HashSet<VirtualMachine>());

        hosts.Add(host);
        await dbContext.SaveChangesAsync();

        response.HostId = host.Id;
        return response;
    }

    public Task DeleteAsync(HostRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<HostResponse.Edit> EditAsync(HostRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public async Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        HostResponse.GetDetail response = new();

        Server? host = await GetHostById(request.HostId)
            .Include(x => x.VirtualisationFactors)
            .ThenInclude(x => x.Processor)
            .Include(x => x.Specifications)
            .Include(x => x.Machines)
            .SingleOrDefaultAsync();

        if (host is null)
        {
            throw new EntityNotFoundException(nameof(Server), request.HostId);
        }

        // Format virtual machines of server. 
        List<VirtualMachineDto.Index>? machineIndexes = null;

        List<VirtualMachine> machines = host.Machines.ToList();
        if (machines is not null)
        {
            machineIndexes = machines.Select(machine =>
            new VirtualMachineDto.Index()
            {
                Id = machine.Id,
                Fqdn = machine.Fqdn,
                Status = machine.Status

            }).ToList();
        }

        // Format virtualisation factors. 
        List<KeyValuePair<ProcessorDto, int>> virtualisationFactors = host.VirtualisationFactors.Select(vf =>
            new KeyValuePair<ProcessorDto, int>(
                new ProcessorDto()
                {
                    Name = vf.Processor.Name,
                    Cores = vf.Processor.Cores,
                    Threads = vf.Processor.Threads,
                },
                vf.Factor
                )
        ).ToList();

        response.Host = new HostDto.Detail
        {
            Id = host.Id,
            Name = host.Name,
            Machines = machineIndexes,
            RemainingResources = new SpecificationsDto()
            {
                Memory = host.RemainingResources.Memory,
                Storage = host.RemainingResources.Storage,
                VirtualProcessors = host.RemainingResources.Processors
            },
            Specifications = new HostSpecificationsDto()
            {
                Memory = host.Specifications.Memory,
                Storage = host.Specifications.Storage,
                Processors = virtualisationFactors
            }
        };

        return response;
    }

    public async Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        HostResponse.GetIndex response = new();
        var query = hosts.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(request.SearchTerm));
        }

        response.TotalAmount = query.Count();

        query = query.OrderByDescending(x => x.CreatedAt);
        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        response.Hosts = await query
           .Select(x => new HostDto.Index
           {
               Id = x.Id,
               Name = x.Name
           }).ToListAsync();

        return response;
    }
}
