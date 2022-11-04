namespace Shared.customer;

public interface IHostService
{
    Task<IEnumerable<CustomerDto.Index>> GetIndexAsync(int offset);
    Task<CustomerDto.Details> GetDetailAsync(long customerId);
    Task<int> GetCount();
}