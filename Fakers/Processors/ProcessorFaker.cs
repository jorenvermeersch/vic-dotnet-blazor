using BogusStore.Fakers.Common;
using Domain.Hosts;

namespace Fakers.Processors;

public class ProcessorFaker : EntityFaker<Processor>
{
    public ProcessorFaker()
    {
        CustomInstantiator(f => new Processor(
            name: "CPU-" + f.Random.Number(1000, 9999),
            cores: f.Random.Number(1, 20),
            threads: f.Random.Number(1, 40)
        ));
    }
}
