namespace Domain.Core;

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
}
