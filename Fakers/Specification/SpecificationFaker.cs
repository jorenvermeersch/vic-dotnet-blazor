using BogusStore.Fakers.Common;
using Domain.Common;

namespace Fakers.Specification;

public class SpecificationFaker : EntityFaker<Specifications>
{
    public SpecificationFaker()
    {
        CustomInstantiator(f => new Specifications(
            processors: f.Random.Number(50, 256),
            memory: f.Random.Number(50, 256),
            storage: f.Random.Number(300, 10000)
        ));
    }
}
