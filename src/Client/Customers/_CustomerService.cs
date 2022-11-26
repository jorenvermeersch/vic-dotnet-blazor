using Shared.Customers;

namespace Client.Customers;

public class CustomerService : ICustomerService
{
    public CustomerService()
    {

    }
    public Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CustomerRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
