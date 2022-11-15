using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Specification;

public class SpecificationService
{
    public readonly List<SpecificationDto> specifications = new();
    
	public SpecificationService()
	{
        var specificationFaker = new Faker<SpecificationDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Storage, f => f.Random.Number(300, 10000))
            .RuleFor(x => x.Memory, f => f.Random.Number(50, 256))
            .RuleFor(x => x.Processors, f => f.Random.Number(50, 256));

        specifications = specificationFaker.Generate(50);

        var remainingSpecipicationFaker = new Faker<SpecificationDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Storage, f => f.Random.Number(2, 300))
            .RuleFor(x => x.Memory, f => f.Random.Number(2, 50))
            .RuleFor(x => x.Processors, f => f.Random.Number(2, 50));

        specifications = specificationFaker.Generate(50);
        specifications.AddRange(remainingSpecipicationFaker.Generate(30));
    }
}
