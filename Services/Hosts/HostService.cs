using Domain.Accounts;
using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.Data;
using Shared.Accounts;
using Shared.Hosts;
using Shared.VirtualMachines;

namespace Services.Hosts;

public class HostService : IHostService
{

    private readonly VicDBContext _dbContext;
    private readonly DbSet<Server> _hosts;

    public HostService(VicDBContext dbContext)
    {
        _dbContext = dbContext;
        _hosts = dbContext.Hosts;
    }

    private IQueryable<Server> GetHostById(long id) => _hosts
               .AsNoTracking()
               .Where(p => p.Id == id);

    public async Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        HostResponse.Create response = new();

        HostDto.Mutate model = request.Host;

        var host = new Server(model.Name, new HostSpecifications(
                model.Specifications.Processors.Select(x =>
                new KeyValuePair<Processor, int>(new Processor(x.Key.Name, x.Key.Cores, x.Key.Threads), x.Value)).ToList(),
                model.Specifications.Storage, model.Specifications.Memory
            ), new HashSet<VirtualMachine>());
        _hosts.Add(host);
        await _dbContext.SaveChangesAsync();
        response.HostId = host.Id;
        return response;
    }

    public async Task DeleteAsync(HostRequest.Delete request)
    {
        _hosts.RemoveIf(host => host.Id == request.HostId);
        await _dbContext.SaveChangesAsync();
    }

    public Task<HostResponse.Edit> EditAsync(HostRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public async Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        HostResponse.GetDetail response = new();

        Server host = await GetHostById(request.HostId).SingleOrDefaultAsync();

        response.Host = new HostDto.Detail
        {
            Id = host.Id,
            Name = host.Name,
            Machines = host.Machines.Select(y => new VirtualMachineDto.Index
            {
                Id = y.Id,
                Fqdn = y.Fqdn,
                Status = y.Status,
            }).ToList(),
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
                Processors = host.Specifications.VirtualisationFactors.Select(y => new KeyValuePair<ProcessorDto, int>(new ProcessorDto() { Cores = y.Key.Cores, Name = y.Key.Name, Threads = y.Key.Threads }, y.Value)).ToList()
            }
        };

        return response;
    }

    public async Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        HostResponse.GetIndex response = new();
        var query = _hosts.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(request.SearchTerm));
        }

        response.TotalAmount = query.Count();

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
