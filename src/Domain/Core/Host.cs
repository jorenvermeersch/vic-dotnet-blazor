namespace Domain.Core;

public abstract class Host<T> : Machine where T : Machine
{
    public Specifications RemainingCapacity { get; private set; }
    public ISet<T> Machines { get; private set; }
    public Host(string name, Specifications specifications, ISet<T> machines)
    : base(name, specifications)
    {
        Machines = machines;
        RemainingCapacity = CalculateRemainingCapacity();
    }

    private Specifications CalculateRemainingCapacity()
    {
        Specifications ms;
        int processors = 0, memory = 0, storage = 0;

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

}
