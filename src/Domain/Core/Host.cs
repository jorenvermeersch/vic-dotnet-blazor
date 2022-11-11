using Ardalis.GuardClauses;

namespace Domain.Core;

public abstract class Host<T> : Machine where T : Machine
{
    public Specifications RemainingResources { get; set; }
    public ISet<T> Machines { get; set; } = new HashSet<T>();

    public Host(string name, Specifications specifications, ISet<T> machines)
    : base(name, specifications)
    {
        Machines = machines;
        RemainingResources = CalculateRemainingResources();
    }

    private Specifications CalculateRemainingResources()
    {
        Specifications ms;
        int processors = 0,
            memory = 0,
            storage = 0;

        foreach (T machine in Machines)
        {
            ms = machine.Specifications;

            processors += ms.Processors;
            memory += ms.Memory;
            storage += ms.Storage;
        }

        // Math.Max as safeguard since remaining capacity must be equal or larger than zero.
        processors = Math.Max(Specifications.Processors - processors, 0);
        memory = Math.Max(Specifications.Memory - memory, 0);
        storage = Math.Max(Specifications.Storage - storage, 0);

        return new Specifications(processors, memory, storage);
    }

    private bool HasResourcesFor(T machine)
    {
        return RemainingResources.HasResourcesFor(machine.Specifications);
    }

    public void Add(T machine)
    {
        Guard.Against.Null(machine, nameof(machine));

        if (Machines.Contains(machine))
        {
            throw new ArgumentException($"{machine.GetType().Name} {machine.Name} already linked to {GetType().Name} {Name}");
        }

        if (!HasResourcesFor(machine))
        {
            throw new ArgumentException($"{GetType().Name} {Name} does not have enough remaining resources");
        }

        Machines.Add(machine);
    }

    public void Remove(T machine)
    {
        Guard.Against.Null(machine, nameof(machine));
        Machines.Remove(machine);
    }
}
