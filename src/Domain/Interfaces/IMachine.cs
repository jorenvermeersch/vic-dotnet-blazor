using Domain.Domain;

namespace Domain.Interfaces;
public interface IMachine
{
    public string Name { get; set; }
    public Resources Resource { get; set; }
}