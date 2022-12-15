using Shared.VirtualMachines;

namespace Client.Extensions;

public static class VirtualMachineExtensions
{
    private static string route = "virtual-machine";

    public static string GetDetailUrl(this VirtualMachineDto.Index machine)
    {
        return $"{route}/{machine.Id}";
    }
}
