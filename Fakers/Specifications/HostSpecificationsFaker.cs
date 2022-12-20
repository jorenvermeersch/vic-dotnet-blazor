using Bogus;
using Domain.Common;
using Domain.Hosts;

namespace Fakers.Specifications;

public class HostSpecificationsFaker : Faker<HostSpecifications>
{
    public HostSpecificationsFaker(List<Processor> processors)
    {
        CustomInstantiator(f => new HostSpecifications(
            processors: new List<KeyValuePair<Processor, int>> { new KeyValuePair<Processor, int>(f.PickRandom(processors), f.Random.Number(1000, 5000)) },
            storage: int.MaxValue,
            memory: int.MaxValue
        ));
    }
}