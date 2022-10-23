namespace Domain;
public class Server : IHost {
    public int Id {get;set;}
    public string Name { get; set; }
    public int VCpu { get; set; }
    public int GbMemory { get; set; }
    public int GbStorage { get; set; }
    public Server(string name, int vCpu, int gbMemory, int gbStorage) {
        Name = name;
        VCpu = vCpu;
        GbMemory = gbMemory;
        GbStorage = gbStorage;
    }
}
