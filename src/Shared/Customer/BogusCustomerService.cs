using Bogus;
using Shared.customer;
using System;
using System.Collections.Generic;

namespace Shared.Customer;

public class BogusCustomerService : ICustomerService
{
    private readonly List<CustomerDto.Details> _customers = new();
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

        _contacts = contactPersonFaker.Generate(100);

        var departments = new[] { "DIT", "DBO", "DBT" };
        var educations = new[] { "", "Toegepaste Informatica", "Bedrijfsmanagement", "Elektro-mechanica" };

        var internalcustomerFaker = new Faker<CustomerDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => customerId++)
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

        _customers = internalcustomerFaker.Generate(30);
        _customers.AddRange(externalcustomerFaker.Generate(15));

        var rnd = new Random();
        _customers = _customers.OrderBy(c => rnd.Next()).ToList();

    }
    Task<CustomerDto.Details> ICustomerService.GetDetailAsync(long customerId)
    {
        return Task.FromResult(_customers.Single(x => x.Id == customerId));
    }

    Task<IEnumerable<CustomerDto.Index>> ICustomerService.GetIndexAsync()
    {
        return Task.FromResult(_customers.Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = x.ContactPerson.Firstname + " " + x.ContactPerson.Lastname
        }));
    }
}
