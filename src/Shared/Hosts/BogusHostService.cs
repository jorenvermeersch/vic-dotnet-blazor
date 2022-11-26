
using Bogus;
using Bogus.DataSets;
using Shared.Customers;
using Shared.Specifications;
using Shared.VirtualMachines;

namespace Shared.Hosts;

public class BogusHostService: IHostService
{
    public readonly List<HostDto.Detail> hosts = new();
    private SpecificationService specificationService = new();

	public BogusHostService()
	{
        
        var hostId = 0;

        var hostFaker = new Faker<HostDto.Detail>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => hostId++)
            .RuleFor(x => x.Name, f => f.Internet.DomainWord())
            .RuleFor(x => x.Specifications, f => f.PickRandom(specificationService.specifications.Take(50)))
            .RuleFor(x => x.RemainingResources, (f, u) => f.PickRandom(specificationService.specifications.Skip(50).Where(x => x.Storage < u.Specifications.Storage && x.Memory < u.Specifications.Memory && x.Processors< u.Specifications.Processors)));

        hosts = hostFaker.Generate(10);
        
    }

    public Task<HostDto.Detail> Add(HostDto.Mutate newHost)
    {
        newHost.Id = hosts.Count + 1;
        HostDto.Detail host = new()
        {
            Id = newHost.Id,
            Name = newHost.Name,
            Specifications = new SpecificationsDto() { 
                Memory = newHost.Specifications.Memory,
                Processors = newHost.Specifications.Processors,
                Storage = newHost.Specifications.Storage
            },
            RemainingResources = new SpecificationsDto()
            {
                Memory = newHost.Specifications.Memory,
                Processors = newHost.Specifications.Processors,
                Storage = newHost.Specifications.Storage

            }
        };
        hosts.Add(host);
        return Task.FromResult(host);
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(hosts.Count());
    }

    public Task<HostDto.Detail> GetDetailAsync(long hostId)
    {
        return Task.FromResult(hosts.Single(x => x.Id == hostId));
    }

    public Task<IEnumerable<HostDto.Index>> GetIndexAsync(int offset)
    {
        return Task.FromResult(hosts.Skip(offset).Take(20).Select(x => new HostDto.Index
        {
            Id = x.Id,
            Name = x.Name
        }));
    }
}
