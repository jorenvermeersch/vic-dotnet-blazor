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
        //[Required(ErrorMessage = "Naam moet ingevuld zijn.")]
        public string Name { get; set; }
        //TODO - validatie van specificaties werkt niet
        public SpecificationDto Specifications { get; set; } = new SpecificationDto();
        public SpecificationDto RemainingResources { get; set; } = new SpecificationDto();
        public List<VirtualMachineDto.Index> Machines { get; set; }
    }

    public class Create
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Processors { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
    }
}