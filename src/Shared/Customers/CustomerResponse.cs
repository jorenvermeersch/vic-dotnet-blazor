using Domain.Customers;

namespace Shared.Customers;

public class CustomerResponse
{
    public class GetIndex
    {
        public List<CustomerDto.Index>? Customers { get; set; }
        public int TotalAmount { get; set; }
    }

    public class GetDetail
    {
        public CustomerDto.Detail Customer { get; set; } = default!;
    }

    public class Delete
    {
    }

    public class Create
    {
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public class Edit
    {
        public long CustomerId { get; set; }
    }
}
