using Ardalis.GuardClauses;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Hosts;

[NotMapped]
public abstract class Host<T> : Machine where T : Machine
{
    #region Fields
    private HostSpecifications _specifications = default!;
    private List<History<Host<T>, T>> _history = new();
    #endregion

    #region Properties
    public new HostSpecifications Specifications
    {
        get => _specifications;
        set
        {
            HostSpecifications current = _specifications;
            _specifications = value;

            // Check if new specifications are able to run existing machines.
            if (RemainingResources.Values.Any(amount => amount < 0))
            {
                _specifications = current;
                throw new ArgumentException(
                    "New specifications are insufficient for running existing machines"
                );
            }
            // UpdateHistory();
        }
    }
    public Specifications RemainingResources => CalculateRemainingResources();
    public ISet<T> Machines { get; set; } = new HashSet<T>();

    [NotMapped]
    public IList<History<Host<T>, T>> History => _history;
    #endregion

    #region Constructors
    protected Host() { }

    public Host(string name, HostSpecifications specifications, ISet<T>? machines)
        : base(name, specifications)
    {
        _specifications = specifications;
        Machines = machines ?? new HashSet<T>();

        // Check if host has enough resources to run all machines.
        Specifications remainingResources = CalculateRemainingResources();

        if (remainingResources.Values.Any(amount => amount < 0))
        {
            throw new ArgumentException(
                $"{GetType().Name} {name} does not have enough resources to support all machines"
            );
        }
        // UpdateHistory();
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

    public void AddMachine(T machine)
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
        // UpdateHistory();
    }

    public void RemoveMachine(T machine)
    {
        Guard.Against.Null(machine, nameof(machine));
        Machines.Remove(machine);
        // UpdateHistory();
    }

    public void AddProcessor(Processor processor, int virtualisationFactor)
    {
        // Check for negative or zero virtualisation factor happens in HostSpecifications.
        _specifications.AddProccessor(processor, virtualisationFactor);
        // UpdateHistory();
    }

    public void RemoveProcessor(Processor processor, int virtualisationFactor)
    {
        if (RemainingResources.Processors < processor.Cores * virtualisationFactor)
        {
            throw new ArgumentException(
                $"{GetType().Name} {Name} does not have enough processors to support all machines"
            );
        }

        _specifications.RemoveProcessor(processor, virtualisationFactor);
        // UpdateHistory();
    }

    public void UpdateHistory()
    {
        _history.Add(new History<Host<T>, T>(this));
    }
    #endregion
}
