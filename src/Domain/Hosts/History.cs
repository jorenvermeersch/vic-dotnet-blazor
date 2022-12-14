using Domain.Common;

namespace Domain.Hosts;

public class History<T, U>
    where T : Host<U>
    where U : Machine
{
    #region Properties
    public T Host { get; } = default!;
    public DateTime Date { get; } = DateTime.UtcNow;
    public Specifications Specifications { get; } = default!;
    public Specifications SpecificationsUsed { get; } = default!;
    #endregion

    #region Constructors
    private History() { }

    public History(T host)
    {
        Host = host;
        Specifications = host.Specifications;
        SpecificationsUsed = CalculateSpecificationsUsed();
    }
    #endregion

    private Specifications CalculateSpecificationsUsed()
    {
        int processorsUsed,
            memoryUsed,
            storageUsed;
        Specifications specifications = Host.Specifications;
        Specifications remainingResources = Host.RemainingResources;

        processorsUsed = specifications.Processors - remainingResources.Processors;
        memoryUsed = specifications.Memory - remainingResources.Memory;
        storageUsed = specifications.Storage - remainingResources.Storage;

        return new Specifications(processorsUsed, memoryUsed, storageUsed);
    }
}
