using Shared.Customers;

namespace Client.Extensions;

public static class CustomerExtentions
{
    private static string route = "customer";

    public static string GetDetailUrl(this CustomerDto.Index customer)
    {
        return $"{route}/{customer.Id}";
    }

    public static string GetEditUrl(this CustomerDto.Index customer)
    {
        return $"{route}/{customer.Id}/edit";
    }

    public static string GetEditUrl(this CustomerDto.Detail customer)
    {
        return $"{route}/{customer.Id}/edit";
    }


}
