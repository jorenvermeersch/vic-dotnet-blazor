
using Bogus;
using Shared.customer;

namespace Shared.Host;

public class BogusHostService
{
    public readonly List<HostDto> hosts = new();

	public BogusHostService()
	{
        var hostId = 0;

        var hostFaker = new Faker<HostDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => hostId++)
            .RuleFor(x => x.Name, f => f.Internet.DomainWord())
            .RuleFor(x => x.Storage, f => f.Random.Number(2, 1000))
            .RuleFor(x => x.Memory, f => f.Random.Number(2, 128))
            .RuleFor(x => x.Processors, f => f.Random.Number(2, 128));

        hosts = hostFaker.Generate(10);
    }
}
