using Bogus;
using Domain.Common;
using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Processors;
using Fakers.VirtualMachines;

namespace Fakers.Specifications;

public class HostSpecificationsFaker : Faker<HostSpecifications>
{
    private readonly IList<Processor> processors = new List<Processor>();
    public HostSpecificationsFaker()
    {
        processors = new ProcessorFaker().UseSeed(1337).Generate(25);

        CustomInstantiator(f => new HostSpecifications(
            processors: new List<KeyValuePair<Processor, int>> { new KeyValuePair<Processor, int>(f.PickRandom(processors), f.Random.Number(1000, 5000)) },
            storage: int.MaxValue,
            memory: int.MaxValue
        ));
    }
}
