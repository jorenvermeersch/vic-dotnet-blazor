using Shared.Specification;
using Shared.VirtualMachine;

namespace Shared.Host;

public static class HostDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Details
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public SpecificationDto Specifications { get; set; }
        public SpecificationDto RemainingResources { get; set; }
        public List<VirtualMachineDto.Index> Machines { get; set; }
    }
}