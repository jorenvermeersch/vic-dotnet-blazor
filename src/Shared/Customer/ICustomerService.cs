namespace Shared.Customer;

public interface ICustomerService
{
    Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request);
    Task<CustomerDto.Detail> GetDetailAsync(long customerId);
    Task<long> CreateAsync(CustomerDto.Mutate model);
    Task EditAsync(long customerId, CustomerDto.Mutate model);
    Task DeleteAsync(long customerId);
}