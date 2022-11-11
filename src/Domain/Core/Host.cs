using Ardalis.GuardClauses;

namespace Domain.Core;

public abstract class Host<T> : Machine where T : Machine
{
    #region Properties
    public new Specifications Specifications
    {
        get => base.Specifications;
        set
        {
            Specifications current = base.Specifications;
            base.Specifications = value;

            // Check if new specifications are able to run existing machines.
            if (CalculateRemainingResources().Values.Any(amount => amount < 0))
            {
                base.Specifications = current;
                throw new ArgumentException(
                    "New specifications are insufficient for running existing machines"
                );
            }
            RemainingResources = CalculateRemainingResources();
        }
    }
    public Specifications RemainingResources { get; set; }
    public ISet<T> Machines { get; set; } = new HashSet<T>();
    #endregion

    #region Constructors
    public Host(string name, Specifications specifications, ISet<T>? machines)
        : base(name, specifications)
    {
        Machines = machines ?? new HashSet<T>();

        // Check if host has enough resources to run all machines.
        Specifications remainingResources = CalculateRemainingResources();

        if (remainingResources.Values.Any(amount => amount < 0))
        {
            throw new ArgumentException(
                $"{GetType().Name} {name} does not have enough resources to support all machines"
            );
        }

        RemainingResources = remainingResources;
    }
    #endregion

    #region Methods
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

        processors = Specifications.Processors - processors;
        memory = Specifications.Memory - memory;
        storage = Specifications.Storage - storage;

        return new Specifications(processors, memory, storage);
    }

    private bool HasResourcesFor(T machine)
    {
        return RemainingResources.HasResourcesFor(machine.Specifications);
    }

    public void UpdateRemainingResources()
    {
        RemainingResources = CalculateRemainingResources();
    }

    public void Add(T machine)
    {
        Guard.Against.Null(machine, nameof(machine));

        if (Machines.Contains(machine))
        {
            throw new ArgumentException(
                $"{machine.GetType().Name} {machine.Name} already linked to {GetType().Name} {Name}"
            );
        }

        if (!HasResourcesFor(machine))
        {
            throw new ArgumentException(
                $"{GetType().Name} {Name} does not have enough remaining resources"
            );
        }

        Machines.Add(machine);
        UpdateRemainingResources();
    }

    public void Remove(T machine)
    {
        Guard.Against.Null(machine, nameof(machine));
        Machines.Remove(machine);
        UpdateRemainingResources();
    }

    #endregion
}
