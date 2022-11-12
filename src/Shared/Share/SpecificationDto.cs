namespace Shared.Specification;

public class SpecificationDto
{
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    public IList<int> Values => new List<int>() { Processors, Memory, Storage };
}