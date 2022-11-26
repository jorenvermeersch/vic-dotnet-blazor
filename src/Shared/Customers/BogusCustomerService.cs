//using Bogus;
//using Domain.Constants;

//namespace Shared.Customers;

//public class BogusCustomerService : ICustomerService
//{
//    public readonly List<CustomerDto.Detail> customers = new();
//    private readonly List<ContactPersonDto> _contacts = new();
//    public BogusCustomerService()
//    {
//        var customerId = 0;

//        var contactPersonFaker = new Faker<ContactPersonDto>("nl")
//            .UseSeed(1337)
//            .RuleFor(x => x.Firstname, f => f.Name.FirstName())
//            .RuleFor(x => x.Lastname, f => f.Name.LastName())
//            .RuleFor(x => x.Email, f => f.Internet.Email())
//            .RuleFor(x => x.Phonenumber, f => f.Phone.PhoneNumber());

//        _contacts = contactPersonFaker.Generate(60);

//        var departments = new[] { "DIT", "DBO", "DBT" };
//        var educations = new[] { "", "Toegepaste Informatica", "Bedrijfsmanagement", "Elektro-mechanica" };

//        var internalcustomerFaker = new Faker<CustomerDto.Detail>("nl")
//            .UseSeed(1337)
//            .RuleFor(x => x.Id, _ => customerId++)
//            .RuleFor(x => x.Institution, f => f.PickRandom(Enum.GetValues(typeof(Institution)).Cast<Institution>().ToArray()))
//            .RuleFor(x => x.Department, f => f.PickRandom(departments))
//            .RuleFor(x => x.Education, f => f.PickRandom(educations))
//            .RuleFor(x => x.ContactPerson, f => f.PickRandom(_contacts))
//            .RuleFor(x => x.BackupContactPerson, f => f.PickRandom(_contacts))
//            .RuleFor(x => x.CustomerType, f => CustomerType.Intern);



//        var types = new[] { "Voka", "Unizo" };

//        var externalcustomerFaker = new Faker<CustomerDto.Detail>("nl")
//            .UseSeed(1337)
//            .RuleFor(x => x.Id, _ => customerId++)
//            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
//            .RuleFor(x => x.CompanyType, f => f.PickRandom(types))
//            .RuleFor(x => x.ContactPerson, f => f.PickRandom(_contacts))
//            .RuleFor(x => x.BackupContactPerson, f => f.PickRandom(_contacts))
//            .RuleFor(x => x.CustomerType, CustomerType.Extern);

//        customers = internalcustomerFaker.Generate(40);
//        customers.AddRange(externalcustomerFaker.Generate(25));

//        var rnd = new Random();
//        customers = customers.OrderBy(c => rnd.Next()).ToList();

//    }

//    Task<CustomerDto.Detail> ICustomerService.GetDetailAsync(long customerId)
//    {
//        return Task.FromResult(customers.Single(x => x.Id == customerId));
//    }

//    Task<IEnumerable<CustomerDto.Index>> ICustomerService.GetIndexAsync(int offset)
//    {
//        return Task.FromResult(customers.Skip(offset).Take(20).Select(x => new CustomerDto.Index
//        {
//            Id = x.Id,
//            Name = x.ContactPerson.Firstname + " " + x.ContactPerson.Lastname,
//            Email = x.ContactPerson.Email
//        }));
//    }
//    Task<int> ICustomerService.GetCount()
//    {
//        return Task.FromResult(customers.Count());
//    }

//    public Task<int> Add(CustomerDto.Mutate newCustomer)
//    {
//        int id = customers.Count + 1;
//        Institution institution;
//        customers.Add(new CustomerDto.Detail()
//        {
//            Id = id,
//            CompanyName = newCustomer.CompanyName,
//            CustomerType = (CustomerType)Enum.Parse(typeof(CustomerType), newCustomer.CustomerType, true),
//            CompanyType = newCustomer.CompanyType,
//            ContactPerson = newCustomer.ContactPerson,
//            BackupContactPerson = newCustomer.BackupContactPerson,
//            Institution = string.IsNullOrEmpty(newCustomer.Institution) ? null : (Institution)Enum.Parse(typeof(Institution), newCustomer.Institution, true),
//            Department = newCustomer.Department,
//            Education = newCustomer.Education
//        });
//        return Task.FromResult(id);
//    }
//}
