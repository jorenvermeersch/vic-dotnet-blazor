using Domain.Interfaces;

namespace Domain.Controllers;
public interface IMachineRepository<T> {
    public ISet<T> Machines { get; }
    public void AddMachine(T machine);
    public T GetMachine(string name);
}
