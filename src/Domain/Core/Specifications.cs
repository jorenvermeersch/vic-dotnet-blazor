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
        Processors = processors;
        Memory = memory;
        Storage = storage;
    }
    #endregion

    public static Specifications Create(int processors, int memory, int storage)
    {
        Guard.Against.NegativeOrZero(processors, nameof(processors));
        Guard.Against.NegativeOrZero(memory, nameof(memory));
        Guard.Against.NegativeOrZero(storage, nameof(storage));

        return new Specifications(processors, memory, storage);
    }
}
