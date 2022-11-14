using System.ComponentModel.DataAnnotations;

namespace Shared.Specification;

public class SpecificationDto
{
    [Required(ErrorMessage = "Aantal vCPU moet ingevuld zijn.")]
    [Range(1, 100, ErrorMessage = "Aantal vCPU is niet gepast.")]
    public int Processors { get; set; }

    [Required(ErrorMessage = "Geheugen moet ingevuld zijn.")]
    [Range(1, 500, ErrorMessage = "Geheugen is niet gepast.")]
    public int Memory { get; set; }

    [Required(ErrorMessage = "Opslag moet ingevuld zijn.")]
    [Range(1, 5000, ErrorMessage = "Geheugen is niet gepast.")]
    public int Storage { get; set; }
}