using Ardalis.GuardClauses;

namespace Domain.Domain;

[ToString]
public class Specifications
{
    #region Properties
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    #endregion

    #region Constructors
    public Specifications(int processors, int memory, int storage)
    {
        Processors = Guard.Against.NegativeOrZero(processors, nameof(processors));
        Memory = Guard.Against.NegativeOrZero(memory, nameof(memory));
        Storage = Guard.Against.NegativeOrZero(storage, nameof(storage));
    }
    #endregion
}
