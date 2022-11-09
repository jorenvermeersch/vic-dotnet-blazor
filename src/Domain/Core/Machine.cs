using Ardalis.GuardClauses;

namespace Domain.Core;

public abstract class Machine : Entity
{
    #region Properties
    public string Name { get; set; }
    public Specifications Specifications { get; set; }
    #endregion

    #region Constructors
    public Machine(string name, Specifications specifications)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Specifications = Guard.Against.Null(specifications, nameof(specifications));
    }
    #endregion
}
