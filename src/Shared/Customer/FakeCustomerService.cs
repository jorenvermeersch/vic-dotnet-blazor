namespace Shared.customer;

public class FakeCustomerService : ICustomerService
{
    private readonly List<CustomerDto.Details> _customers = new();
    public FakeCustomerService()
    {
        _customers.Add(new()
        {
            Id = 1,
            CompanyName = "VOKA",
            CompanyType = "Voka",
            ContactPerson = new ContactPersonDto()
            {
                Id = 1,
                Firstname = "Jennifer",
                Lastname = "De Donder",
                Phonenumber = "+3256859572",
                Email = "jennifer.dedonder@voka.be"
            },
            BackupContactPerson = new ContactPersonDto()
            {
                Id = 2,
                Firstname = "Jaden",
                Lastname = "Smith",
                Phonenumber = "+3256859856",
                Email = "jaden.smith@voka.be"
            },

        });
        _customers.Add(new()
        {
            Id = 2,
            Education = "Toegepaste Informatica",
            Department = "DIT",
            ContactPerson = new ContactPersonDto()
            {
                Id = 3,
                Firstname = "Kerem",
                Lastname = "Yilmaz",
                Phonenumber = "+3256887672",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            BackupContactPerson = new ContactPersonDto()
            {
                Id = 4,
                Firstname = "Robin",
                Lastname = "Vermeir",
                Phonenumber = "+3256859021",
                Email = "robin.vermeir@hogent.be"
            },

        });
        _customers.Add(new()
        {
            Id = 3,
            Education = "Toegepaste Informatica",
            Department = "DIT",
            ContactPerson = new ContactPersonDto()
            {
                Id = 4,
                Firstname = "Robin",
                Lastname = "Vermeir",
                Phonenumber = "+3256859021",
                Email = "robin.vermeir@hogent.be"
            },

        });
    }

    public Task<CustomerDto.Details> Add(CustomerDto.Details newCustomer)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(_customers.Count());
    }

    Task<CustomerDto.Details> ICustomerService.GetDetailAsync(long customerId)
    {
        return Task.FromResult(_customers.Single(x => x.Id == customerId));
    }

    Task<IEnumerable<CustomerDto.Index>> ICustomerService.GetIndexAsync(int offset)
    {
        return Task.FromResult(_customers.Skip(offset).Take(20).Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = x.ContactPerson.Firstname + " " + x.ContactPerson.Lastname
        }));
    }
}
