namespace Domain.Common;

public class Specifications
{
    #region Properties
    public virtual int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    public IReadOnlyList<int> Values => new List<int>() { Processors, Memory, Storage };
    #endregion

    #region Constructors
    protected Specifications() { }

    public Specifications(int memory, int storage)
    {
        Memory = memory;
        Storage = storage;
    }

    public Specifications(int processors, int memory, int storage)
    {
        Processors = processors;
        Memory = memory;
        Storage = storage;
    }
    #endregion

    #region Methods
    public virtual bool HasResourcesFor(Specifications specs)
    {
        return Processors >= specs.Processors && Memory >= specs.Memory && Storage >= specs.Storage;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not Specifications other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return other.Processors == Processors && other.Memory == Memory && other.Storage == Storage;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}
