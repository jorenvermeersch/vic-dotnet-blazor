using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core;

public abstract class Machine : Entity
{
    #region Properties
    public string Name { get; set; }

    [NotMapped]
    public Specifications Specifications { get; set; }
    #endregion

    #region Constructors

    public Machine() { }

    public Machine(string name, Specifications specifications)
    {
        Name = name;
        Specifications = specifications;
    }
    #endregion

    #region Methods
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not Machine other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (other.Name.Equals(Name) && other.Specifications.Equals(Specifications))
            return true;

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}
