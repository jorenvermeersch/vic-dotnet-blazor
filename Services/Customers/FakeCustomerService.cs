using Shared.Customers;

namespace Services.Customers;

public class FakeCustomerService : ICustomerService
{
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
