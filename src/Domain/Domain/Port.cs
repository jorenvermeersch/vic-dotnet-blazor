namespace Domain.Domain;
public class Port
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
