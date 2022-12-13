namespace Domain.VirtualMachines;

public class Port : Entity
{
    #region Properties
    public int Number { get; set; }
    public string Service { get; set; } = default!;
    #endregion

    #region Constructors
    private Port() { }

    public Port(int number, string service)
    {
        Number = number;
        Service = service;
    }
    #endregion
}
