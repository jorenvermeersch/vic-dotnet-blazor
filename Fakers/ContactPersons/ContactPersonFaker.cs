using BogusStore.Fakers.Common;
using Domain.Customers;

namespace Fakers.ContactPersons;

public class ContactPersonFaker : EntityFaker<ContactPerson>
{
    public ContactPersonFaker(bool isBackupContact = false)
    {
        CustomInstantiator(f => new ContactPerson(
            firstname: f.Name.FirstName(),
            lastname: f.Name.LastName(),
            email: (isBackupContact ? "backup" : "") + f.Internet.Email(),
            phoneNumber: (isBackupContact ? "1" : "") + f.Phone.PhoneNumber()
        ));
    }
}
