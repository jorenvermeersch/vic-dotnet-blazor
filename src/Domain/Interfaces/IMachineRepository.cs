namespace Domain.Interfaces;

public interface IMachineRepository<T>
{
    public ISet<T> Machines { get; }
    public void AddMachine(T machine);
}
