namespace Shared;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto.Index>> GetIndexAsync();
    Task<CustomerDto.Details> GetInternalDetailAsync(long customerId);
    Task<CustomerDto.Details> GetExternalDetailAsync(long customerId);
}