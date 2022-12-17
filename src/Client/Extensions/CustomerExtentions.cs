using Domain.Constants;
using Shared.Customers;

namespace Client.Extensions;

public static class CustomerExtentions
{
    private static string route = "customer";

    public static string GetFullName(this CustomerDto.Detail customer)
    {
        return $"{customer.ContactPerson.Firstname} {customer.ContactPerson.Lastname}";
    }
    public static string GetFullName(this ContactPersonDto contact)
    {
        return $"{contact.Firstname} {contact.Lastname}";
    }


    public static bool IsInternal(this CustomerDto.Detail customer)
    {
        return customer.CustomerType == CustomerType.Intern;
    }

    public static bool IsInternal(this CustomerDto.Mutate customer)
    {
        return customer.CustomerType == CustomerType.Intern.ToString();
    }

    public static bool IsExternal(this CustomerDto.Mutate customer)
    {
        return customer.CustomerType == CustomerType.Extern.ToString();
    }

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
