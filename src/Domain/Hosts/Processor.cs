namespace Domain.Hosts;

public class Processor : Entity
{
    #region Properties
    public string Name { get; set; }
    public int Cores { get; set; }
    public int Threads { get; set; }
    #endregion

    #region Constructors
    public Processor(string name, int cores, int threads)
    {
        Name = name;
        Cores = cores;
        Threads = threads;
    }
    #endregion
}
