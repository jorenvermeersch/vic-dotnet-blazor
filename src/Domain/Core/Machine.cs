using Domain.Domain;

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
        Name = name;
        Specifications = specifications;
    }
    #endregion
}
