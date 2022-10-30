using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.customer;

public class FakeCustomerService : ICustomerService
{
    private readonly List<CustomerDto.Details> _customers = new();
    public FakeCustomerService()
    {
        _customers.Add(new()
        {
            Id = 1,
            Name = "VOKA",
            Type = "Voka",
            ContactPersonDto = new ContactPersonDto()
            {
                Id = 1,
                Firstname = "Jennifer",
                Lastname = "De Donder",
                Phonenumber = "+3256859572",
                Email = "jennifer.dedonder@voka.be"
            },
            BackupContactPersonDto = new ContactPersonDto()
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
            ContactPersonDto = new ContactPersonDto()
            {
                Id = 3,
                Firstname = "Kerem",
                Lastname = "Yilmaz",
                Phonenumber = "+3256887672",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            BackupContactPersonDto = new ContactPersonDto()
            {
                Id = 4,
                Firstname = "Robin",
                Lastname = "Vermeire",
                Phonenumber = "+3256859021",
                Email = "robin.vermeire@hogent.be"
            },

        });
        _customers.Add(new()
        {
            Id = 3,
            Education = "Toegepaste Informatica",
            Department = "DIT",
            ContactPersonDto = new ContactPersonDto()
            {
                Id = 4,
                Firstname = "Robin",
                Lastname = "Vermeire",
                Phonenumber = "+3256859021",
                Email = "robin.vermeire@hogent.be"
            },

        });
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
            Name = x.ContactPersonDto.Firstname + " " + x.ContactPersonDto.Lastname
        }));
    }
}
