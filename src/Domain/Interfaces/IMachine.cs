using Domain.Domain;
using System.Reflection;

namespace Domain.Interfaces;
public interface IMachine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Resource Resource { get; set; }
}