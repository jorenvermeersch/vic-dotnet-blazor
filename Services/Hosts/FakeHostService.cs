using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;
using Services.FakeInitializer;
using Shared.Hosts;
using Shared.VirtualMachines;

namespace Services.Hosts;

public class FakeHostService : IHostService
{
    public static List<Server> Hosts { get; private set; } = new List<Server>();

    static FakeHostService()
    {
        Hosts = FakeInitializerService.FakeHosts ?? new List<Server>();
    }

    public async Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        HostResponse.Create response = new();

        var model = request.Host;

        var host = new Server(model.Name, new HostSpecifications(
                model.Specifications.Processors.Select(x =>
                new VirtualisationFactor(new Processor(x.Key.Name, x.Key.Cores, x.Key.Threads), x.Value)).ToList(),
                model.Specifications.Storage, model.Specifications.Memory
            ), new HashSet<VirtualMachine>())
        {
            Id = Hosts.Max(x => x.Id) + 1,
        };

        response.HostId = host.Id;

        Hosts.Add(host);
        return response;
    }

    public async Task DeleteAsync(HostRequest.Delete request)
    {
        var host = Hosts.SingleOrDefault(x => x.Id == request.HostId);
        if (host != null)
            Hosts.Remove(host);
    }

    public async Task<HostResponse.Edit> EditAsync(HostRequest.Edit request)
    {
        HostResponse.Edit response = new();
        var host = Hosts.SingleOrDefault(x => x.Id == request.HostId);

        if (host == null)
        {
            response.HostId = -1;
            return response;
        }

        var model = request.Host;

        host.Name = model.Name;
        host.Specifications = new HostSpecifications(
                model.Specifications.Processors.Select(x =>
                new VirtualisationFactor(new Processor(x.Key.Name, x.Key.Cores, x.Key.Threads), x.Value)).ToList(),
                model.Specifications.Storage, model.Specifications.Memory);

        response.HostId = host.Id;
        return response;
    }

    public async Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        HostResponse.GetDetail response = new();

        response.Host = Hosts.Where(x => x.Id == request.HostId).Select(x => new HostDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Machines = x.Machines.Select(y => new VirtualMachineDto.Index
            {
                Id = y.Id,
                Fqdn = y.Fqdn,
                Status = y.Status,
            }).ToList(),
            RemainingResources = new SpecificationsDto()
            {
                Memory = x.RemainingResources.Memory,
                Storage = x.RemainingResources.Storage,
                VirtualProcessors = x.RemainingResources.Processors
            },
            Specifications = new HostSpecificationsDto()
            {
                Memory = x.Specifications.Memory,
                Storage = x.Specifications.Storage,
                Processors = x.Specifications.VirtualisationFactors.Select(y => new KeyValuePair<ProcessorDto, int>(new ProcessorDto() { Cores = y.Processor.Cores, Name = y.Processor.Name, Threads = y.Processor.Threads }, y.Factor)).ToList(),
            }
        }).SingleOrDefault() ?? new HostDto.Detail();


        return response;
    }

    public async Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        var query = Hosts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = query.Count();

        var items = query
           .Skip((request.Page - 1) * request.Amount)
           .Take(request.Amount)
           .OrderBy(x => x.Id)
           .Select(x => new HostDto.Index
           {
               Id = x.Id,
               Name = x.Name
           }).ToList();

        var result = new HostResponse.GetIndex
        {
            Hosts = items,
            TotalAmount = totalAmount
        };

        return result;
    }
}
