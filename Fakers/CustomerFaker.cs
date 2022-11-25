using BogusStore.Fakers.Common;
using Domain.Constants;
using Domain.Customers;
using Fakers.ContactPersons;

namespace Fakers;

public static class CustomerFaker
{

    public class ExternalCustomerFaker : EntityFaker<ExternalCustomer>
    {
        public ExternalCustomerFaker(string locale = "nl")
        {
            var types = new[] { "Voka", "Unizo" };

            //TODO: Contactperson faker
            ContactPersonFaker contactPersonFaker = new ContactPersonFaker();
            List<ContactPerson> contacts = contactPersonFaker.Generate(50);

            CustomInstantiator(f => new ExternalCustomer(
                name: f.Company.CompanyName(),
                type: f.PickRandom(types),
                contactPerson: f.PickRandom(contacts),
                backupContact: f.PickRandom(contacts)
            ));
        }
    }


    public class InternalCustomerFaker : EntityFaker<InternalCustomer>
    {
        public InternalCustomerFaker(string locale = "nl")
        {
            var departments = new[] { "DIT", "DBO", "DBT" };
            var educations = new[] { "", "Toegepaste Informatica", "Bedrijfsmanagement", "Elektro-mechanica" };

            //TODO: Contactperson faker
            ContactPersonFaker contactPersonFaker = new ContactPersonFaker();
            List<ContactPerson> contacts = contactPersonFaker.Generate(50);

            CustomInstantiator(f => new InternalCustomer(
                institution: f.PickRandom(Enum.GetValues(typeof(Institution)).Cast<Institution>().ToList()),
                department: f.Company.CompanyName(),
                education: f.PickRandom(educations),
                contactPerson: f.PickRandom(contacts),
                backupContact: f.PickRandom(contacts)
            ));
        }
    }


}
