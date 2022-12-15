using Shared.Hosts;

namespace Client.Extensions;

public static class HostExtensions
{
    private static string route = "host";

    public static string GetDetailUrl(this HostDto.Index host)
    {
        return $"{route}/{host.Id}";
    }
}
