namespace Shared.Customer;

public class CustomerResult
{
    public class Index
    {
        public List<CustomerDto.Index>? Customers { get; set; }
        public int TotalAmount { get; set; }
    }
}
