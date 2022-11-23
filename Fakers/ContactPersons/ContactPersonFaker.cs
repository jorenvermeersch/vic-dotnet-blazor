using BogusStore.Fakers.Common;
using Domain.Customers;

namespace Fakers.ContactPersons;

public class ContactPersonFaker : EntityFaker<ContactPerson>
{
    public ContactPersonFaker()
    {
        CustomInstantiator(f => new ContactPerson(
            firstname: f.Name.FirstName(),
            lastname: f.Name.LastName(),
            email: f.Internet.Email(),
            phoneNumber: f.Phone.PhoneNumber()
        ));
    }
}
