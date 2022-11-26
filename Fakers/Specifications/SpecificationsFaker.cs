using Bogus;

namespace Fakers.Specifications;

public class SpecificationsFaker : Faker<Domain.Common.Specifications>
{
    public SpecificationsFaker()
    {
        base.CustomInstantiator(f => new Domain.Common.Specifications(
            processors: f.Random.Number(50, 256),
            memory: f.Random.Number(50, 256),
            storage: f.Random.Number(300, 10000)
        ));
    }
}
