namespace Domain.Domain;
public class Resource {
    #region Properties
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    #endregion
    #region Constructors
    public Resource(int processors, int memory, int storage) {
        Processors = processors;
        Memory = memory;
        Storage = storage;
    }
    #endregion
}
