using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Web;

namespace Client.Extensions;

public static class ObjectExtensions
{
    public static string GetQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString());

        return string.Join("&", properties.ToArray());
    }

    public static string GbFormat(this string input)
    {
        return $"{input} GB";
    }

    public static string FormatIfEmpty(this string? input)
    {
        return input.IsNullOrEmpty() ? "-" : input!;
    }

    public static string FormatDate(this DateTime date, string format = "d MMMM yyyy")
    {
        var cultureInfo = new CultureInfo("nl-BE");
        return date.ToString(format, cultureInfo);
    }
}
