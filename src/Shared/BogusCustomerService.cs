using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class BogusCustomerService : ICustomerService
{
    private readonly List<CustomerDto.Details> _customers = new();

    public BogusCustomerService()
    {
        /*var customerIds = 0;
        var customerFaker = new Faker<CustomerDto.Details>("nl")
        .UseSeed(1337) // Always return the same products
        .RuleFor(x => x.Id, _ => ++customerIds)
        .RuleFor(x => x.Department, f => f.dep)
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Image, f => f.Internet.Avatar())
        .RuleFor(x => x.Price, f => f.Random.Decimal(0, 250));
        _products = productFaker.Generate(25);*/
    }
    public Task<CustomerDto.Details> GetExternalDetailAsync(long customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerDto.Index>> GetIndexAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto.Details> GetInternalDetailAsync(long customerId)
    {
        throw new NotImplementedException();
    }
}
