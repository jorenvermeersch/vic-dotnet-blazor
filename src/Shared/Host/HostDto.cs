using Shared.Specification;
using Shared.VirtualMachine;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

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
        public SpecificationDto Specifications { get; set; } = new SpecificationDto();
        public SpecificationDto RemainingResources { get; set; } = new SpecificationDto();
        public List<VirtualMachineDto.Index> Machines { get; set; }
    }

    public class Create
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SpecificationDto Specifications { get; set; } = new();
    }
}