namespace Domain.Core;

[ToString]
public class Port : Entity
{
    #region Properties
    public int Number { get; set; }
    public string Service { get; set; }
    #endregion

    #region Constructors
    public Port(int number, string service)
    {
        Number = number;
        Service = service;
    }
    #endregion
}
