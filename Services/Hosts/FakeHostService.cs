using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Hosts;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

namespace Services.Hosts;

public class FakeHostService : IHostService
{
    private static readonly List<Server> hosts = new List<Server>();

    static FakeHostService() 
    {
        hosts = new HostFaker().UseSeed(1337).Generate(50);
    }

    public Task<HostResponse.Create> CreateAsync(HostRequest.Create request)
    {
        throw new NotImplementedException();
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
        }).SingleOrDefault();

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
