

using Domain.Args;
using Domain.Domain;
using Domain.Interfaces;

namespace Domain.Controllers;

public class CustomerController
{
    private VIC _vic = VIC.Instance;

    public ISet<ICustomer> GetAllCustomers()
    {
        return _vic.CustomerRepo.Customers;
    }

    public ICustomer GetCustomerByEmail(string email)
    {
        return _vic.CustomerRepo.GetCustomerByEmail(email);
    }

    public void CreateAccount(AccountArgs args)
    {
        _vic.CreateAccount(args);
    }
}
