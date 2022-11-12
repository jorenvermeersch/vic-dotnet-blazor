using Bogus;
using Shared.customer;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Shared.Customer;

public class BogusCustomerService : ICustomerService
{
    public readonly List<CustomerDto.Details> customers = new();
    private readonly List<ContactPersonDto> _contacts = new();
    public BogusCustomerService()
    {
        var customerId = 0;

        var contactPersonFaker = new Faker<ContactPersonDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Firstname, f => f.Name.FirstName())
            .RuleFor(x => x.Lastname, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Phonenumber, f => f.Phone.PhoneNumber());

        _contacts = contactPersonFaker.Generate(60);

        var departments = new[] { "DIT", "DBO", "DBT" };
        var educations = new[] { "", "Toegepaste Informatica", "Bedrijfsmanagement", "Elektro-mechanica" };

        var internalcustomerFaker = new Faker<CustomerDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => customerId++)
            .RuleFor(x => x.Institution, f => "Hogent")
            .RuleFor(x => x.Department, f => f.PickRandom(departments))
            .RuleFor(x => x.Education, f => f.PickRandom(educations))
            .RuleFor(x => x.ContactPerson, f => f.PickRandom(_contacts))
            .RuleFor(x => x.BackupContactPerson, f => f.PickRandom(_contacts));



        var types = new[] { "Voka", "Unizo" };

        var externalcustomerFaker = new Faker<CustomerDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => customerId++)
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.Type, f => f.PickRandom(types))
            .RuleFor(x => x.ContactPerson, f => f.PickRandom(_contacts))
            .RuleFor(x => x.BackupContactPerson, f => f.PickRandom(_contacts));

        customers = internalcustomerFaker.Generate(40);
        customers.AddRange(externalcustomerFaker.Generate(25));

        var rnd = new Random();
        customers = customers.OrderBy(c => rnd.Next()).ToList();

    }

    Task<CustomerDto.Details> ICustomerService.GetDetailAsync(long customerId)
    {
        return Task.FromResult(customers.Single(x => x.Id == customerId));
    }

    Task<IEnumerable<CustomerDto.Index>> ICustomerService.GetIndexAsync(int offset)
    {
        return Task.FromResult(customers.Skip(offset).Take(20).Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = x.ContactPerson.Firstname + " " + x.ContactPerson.Lastname
        }));
    }
    Task<int> ICustomerService.GetCount()
    {
        return Task.FromResult(customers.Count());
    }

    public void CreateCustomer(CustomerDto.Details newCustomer)
    {
        customers.Add(newCustomer);
    }
}
