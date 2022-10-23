namespace Domain;
public interface IMachine {
    public int Id { get; set; }
    public string Name { get; set; }
    public int VCpu { get; set; }
    public int GbMemory { get; set; }
    public int GbStorage { get; set; }
}