

using Domain.Args;
using Domain.Domain;
using Domain.Interfaces;

namespace Domain.Controllers;

public class CustomerController
{
    #region Fields
    private VIC _vic = VIC.Instance;
    #endregion

    #region Methods
    public ISet<ICustomer> GetAll()
    {
        return _vic.CustomerRepository.Customers;
    }

    public ICustomer GetById()
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }

    public void Add(CustomerArgs args)
    {
        throw new NotImplementedException();
        // TODO: Implement method.
    }

    public void UpdateById(long id, CustomerArgs args)
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }
    #endregion
}
