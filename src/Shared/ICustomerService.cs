namespace Shared;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto.Index>> GetIndexAsync();
    Task<CustomerDto.Details> GetDetailAsync(long customerId);
}