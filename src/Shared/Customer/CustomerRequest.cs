namespace Shared.Customer;

public class CustomerRequest
{
    public class Index
    {
        public string? SearchTerm { get; set; }
        public string? CustomerType { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;

    }
}
