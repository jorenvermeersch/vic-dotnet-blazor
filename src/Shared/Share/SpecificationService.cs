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
            .RuleFor(x => x.Storage, f => f.Random.Number(2, 1000))
            .RuleFor(x => x.Memory, f => f.Random.Number(2, 128))
            .RuleFor(x => x.Processors, f => f.Random.Number(2, 128));

        specifications = specificationFaker.Generate(50);
    }
}
