using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class Machine : Entity
{
    #region Properties
    public string Name { get; set; } = default!;

    [NotMapped]
    public Specifications Specifications { get; set; } = default!;
    #endregion

    #region Constructors

    protected Machine() { }

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
