namespace Domain.Hosts;

public class Processor : Entity
{
    #region Properties
    public string Name { get; set; } = default!;
    public int Cores { get; set; } = default!;
    public int Threads { get; set; } = default!;
    #endregion

    public List<VirtualisationFactor> VirtualisationFactors { get; set; } = new();

    #region Constructors
    private Processor() { }

    public Processor(string name, int cores, int threads)
    {
        Name = name;
        Cores = cores;
        Threads = threads;
    }
    #endregion

    #region Methods
    public override bool Equals(object? obj)
    {
        bool baseEquals = base.Equals(obj);

        if (obj is not Processor other)
            return false;

        return baseEquals && Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}
