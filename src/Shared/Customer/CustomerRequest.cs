namespace Shared.Customer;

public class CustomerRequest
{
    public class GetIndex
    {
        public string? SearchTerm { get; set; }
        public string? CustomerType { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;

    }

    public class GetDetail
    {
        public long CustomerId { get; set; }
    }

    public class Delete
    {
        public long CustomerId { get; set; }
    }

    public class Create
    {
        public CustomerDto.Mutate Customer { get; set; } = default!;
    }

    public class Edit
    {
        public long CustomerId { get; set; }
        public CustomerDto.Mutate Customer { get; set; } = default!;
    }
}
