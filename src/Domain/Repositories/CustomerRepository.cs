

using Domain.Domain;
using Domain.Interfaces;

namespace Domain.Repositories;

public class CustomerRepository
{
    private readonly ISet<ICustomer> _customers = new HashSet<ICustomer>();
    public ISet<ICustomer> Customers => _customers;

    public CustomerRepository()
    {
        SeedData();
    }

    internal void AddExternalCustomer(ExternalCustomer customer)
    {
        _customers.Add(customer);
    }
    internal void AddInternalCustomer(InternalCustomer customer)
    {
        _customers.Add(customer);
    }

    internal ICustomer GetCustomerByEmail(string email)
    {
        return _customers.First(c => c.ContactPerson.Email == email);
    }

    private void SeedData()
    {
        _customers.Add(new ExternalCustomer("Unizoo", "Unizo", new ContactPerson("Giles", "Van Wetteren", "giles.vanwetteren@unizo.be", "+3258691425"), new ContactPerson("Pieter", "Selie", "pieter.selie@unizo.be", "+3202691855")));
        _customers.Add(new ExternalCustomer("Vookaa", "Voka", new ContactPerson("Ali", "Mohammed", "ali.Mohammed@voka.be", "+3258691102"), new ContactPerson("Jen", "eever", "jen.eever@voka.be", "+3202862855")));
        _customers.Add(new InternalCustomer("Verpleegkunde", "vp", new ContactPerson("Robin", "Vermeire", "robin.vermeire@student.hogent.be", "+32471185226")));
        _customers.Add(new InternalCustomer("", "DIT", new ContactPerson("Joren", "Vermeersch", "joren.vermeersh@hogent.be", "+325869253"), new ContactPerson("Kerem", "Yilmaz", "Kerem.Yilmaz@hogent.be", "+325885253")));
    }
}
