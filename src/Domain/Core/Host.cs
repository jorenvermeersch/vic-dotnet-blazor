using Domain.Domain;

namespace Domain.Core;

public abstract class Host : Machine
{
    public Host(string name, Specifications specifications) : base(name, specifications) { }
}
