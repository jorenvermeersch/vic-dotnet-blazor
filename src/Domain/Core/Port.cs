using Ardalis.GuardClauses;

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
        Number = Guard.Against.NegativeOrZero(number, nameof(number));
        Service = Guard.Against.NullOrWhiteSpace(service, nameof(service));
    }
    #endregion
}
