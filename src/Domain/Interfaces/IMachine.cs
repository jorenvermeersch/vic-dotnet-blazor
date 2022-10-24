namespace Domain.Interfaces;
public interface IMachine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
}