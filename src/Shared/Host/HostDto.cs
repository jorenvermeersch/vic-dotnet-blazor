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
    public class Details
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HostSpecificationsDto Specifications { get; set; } = new();
        public SpecificationDto RemainingResources { get; set; } = new();
        public List<VirtualMachineDto.Index> Machines { get; set; } = new();
    }

    public class Create
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SpecificationDto Specifications { get; set; } = new();
    }
}