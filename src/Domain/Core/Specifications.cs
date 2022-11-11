﻿namespace Domain.Core;

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

    #region Methods
    public bool HasResourcesFor(Specifications specs)
    {
        return (Processors >= specs.Processors) && (Memory >= specs.Memory) && (Storage >= specs.Memory);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not Specifications other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return (other.Processors == Processors) && (other.Memory == Memory) && (other.Storage == Storage);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion


}
