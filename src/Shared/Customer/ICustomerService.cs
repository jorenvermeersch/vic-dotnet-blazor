using System.Threading.Tasks;

namespace Shared.Customer;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto.Index>> GetIndexAsync(int offset);
    Task<CustomerDto.Details> GetDetailAsync(long customerId);
    Task<int> GetCount();

    Task<int> Add(CustomerDto.Create newCustomer);
}