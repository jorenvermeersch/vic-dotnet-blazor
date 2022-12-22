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
    public class GetAllDetail
    {
        public List<CustomerDto.Detail> Customers { get; set; } = default!;
        public int TotalAmount { get; set; }
    }

    public class Delete
    {
    }

    public class Create
    {
        public long CustomerId { get; set; }
    }

    public class Edit
    {
        public long CustomerId { get; set; }
    }
}
