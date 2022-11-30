using Domain.Accounts;
using Domain.Customers;
using Services.FakeInitializer;
using Shared.Customers;

namespace Services.Customers;

public class FakeCustomerService : ICustomerService
{
    private static readonly List<Customer> customers = new();

    static FakeCustomerService()
    {
        customers = FakeInitializerService.FakeCustomers ?? new List<Customer>();
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
