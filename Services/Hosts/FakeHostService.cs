using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Hosts;
using Services.FakeInitializer;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;
using Domain.Common;

namespace Services.Hosts;

public class FakeHostService : IHostService
{
    private static readonly List<Server> hosts = new List<Server>();

    static FakeHostService() 
    {
        hosts = FakeInitializerService.FakeHosts ?? new List<Server>();
    }

    public async Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        HostResponse.Create response = new();

        var model = request.Host;

        var host = new Server(model.Name, new HostSpecifications(
                model.Specifications.Processors.Select(x =>
                new KeyValuePair<Processor, int>(new Processor(x.Key.Name, x.Key.Cores, x.Key.Threads), x.Value)).ToList(),
                model.Specifications.Storage, model.Specifications.Memory
            ), new HashSet<VirtualMachine>())
        {
            Id = hosts.Max(x => x.Id) + 1,
        };

        response.HostId = host.Id;

        hosts.Add(host);
        return response;
    }

    public Task DeleteAsync(HostRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public async Task<HostResponse.Edit> EditAsync(HostRequest.Edit request)
    {
        HostResponse.Edit response = new();
        var host = hosts.SingleOrDefault(x => x.Id == request.HostId);

        if (host == null)
        {
            response.HostId = -1;
            return response;
        }

        var model = request.Host;

        host.Name = model.Name;
        host.Specifications = new HostSpecifications(
                model.Specifications.Processors.Select(x =>
                new KeyValuePair<Processor, int>(new Processor(x.Key.Name, x.Key.Cores, x.Key.Threads), x.Value)).ToList(),
                model.Specifications.Storage, model.Specifications.Memory);

        response.HostId = host.Id;
        return response;
    }

    public async Task<HostResponse.GetDetail> GetDetailAsync(HostRequest.GetDetail request)
    {
        HostResponse.GetDetail response = new();

        response.Host = hosts.Where(x => x.Id == request.HostId).Select(x => new HostDto.Detail
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
            Specifications = new HostSpecificationsDto() {
                Memory = x.Specifications.Memory,
                Storage = x.Specifications.Storage,
                Processors = x.Specifications.VirtualisationFactors.Select(y => new KeyValuePair<ProcessorDto, int>(new ProcessorDto() { Cores = y.Key.Cores, Name = y.Key.Name, Threads = y.Key.Threads}, y.Value)).ToList()
            }
        }).SingleOrDefault() ?? new HostDto.Detail();


        return response;
    }

    public async Task<HostResponse.GetIndex> GetIndexAsync(HostRequest.GetIndex request)
    {
        var query = hosts.AsQueryable();

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
