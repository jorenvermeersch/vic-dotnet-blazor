namespace Shared.customer;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto.Index>> GetIndexAsync(int offset);
    Task<CustomerDto.Details> GetDetailAsync(long customerId);

    Task<int> GetCount();

    void CreateCustomer(CustomerDto.Details newCustomer);
}