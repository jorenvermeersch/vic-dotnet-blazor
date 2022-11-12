
using Bogus;
using Bogus.DataSets;
using Shared.customer;
using Shared.Specification;
using Shared.VirtualMachine;

namespace Shared.Host;

public class BogusHostService: IHostService
{
    public readonly List<HostDto.Details> hosts = new();
    private SpecificationService specificationService = new();

	public BogusHostService()
	{
        
        var hostId = 0;

        var hostFaker = new Faker<HostDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => hostId++)
            .RuleFor(x => x.Name, f => f.Internet.DomainWord())
            .RuleFor(x => x.Specifications, f => f.PickRandom(specificationService.specifications))
            .RuleFor(x=>x.RemainingResources, f=>f.PickRandom(specificationService.specifications));

        hosts = hostFaker.Generate(10);
    }

    public void CreateHost(HostDto.Details newHost)
    {
        hosts.Add(newHost);
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(hosts.Count());
    }

    public Task<HostDto.Details> GetDetailAsync(long hostId)
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
