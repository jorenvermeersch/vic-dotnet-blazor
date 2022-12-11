using Domain.Common;

namespace Domain.Hosts;

public class History<T, U>
    where T : Host<U>
    where U : Machine
{
    #region Properties
    public T Host { get; private set; }
    public DateTime Date { get; private set; } = DateTime.UtcNow;
    public Specifications Specifications { get; private set; }
    public Specifications SpecificationsUsed { get; private set; }
    #endregion


    #region Constructors
    public History() { }

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
