using Domain.Interfaces;

namespace Domain.Domain;
public class Server : IHost
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    #endregion
    #region Constructors
    public Server(string name, int vCpu, int gbMemory, int gbStorage)
    {
        Name = name;
        Processors = vCpu;
        Memory = gbMemory;
        Storage = gbStorage;
    }
    #endregion
}
