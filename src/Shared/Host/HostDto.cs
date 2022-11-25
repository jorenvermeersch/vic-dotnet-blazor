using Shared.Specification;
using Shared.VirtualMachine;

namespace Shared.Host;

public static class HostDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
    }
    public class Detail
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public HostSpecificationsDto Specifications { get; set; } = default!;
        public SpecificationDto RemainingResources { get; set; } = default!;
        public List<VirtualMachineDto.Index>? Machines { get; set; }
    }

    public class Mutate
    {
        public string Name { get; set; } = default!;
        public HostSpecificationsDto Specifications { get; set; } = default!;
    }
}