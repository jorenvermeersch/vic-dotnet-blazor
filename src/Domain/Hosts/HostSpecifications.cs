﻿using Ardalis.GuardClauses;
using Domain.Hosts;

namespace Domain.Common;

public class HostSpecifications : Specifications
{
    #region Properties
    public Dictionary<Processor, int> VirtualisationFactors { get; set; } = new(); // Key : Value => Processor : Virtualisation factor.
    public new int Processors =>
        VirtualisationFactors.Select(pair => pair.Key.Cores * pair.Value).Sum(); // Sum of: Cores * virtualisation factor.
    #endregion

    #region Constructors
    public HostSpecifications(Dictionary<Processor, int> processors, int storage, int memory)
        : base(memory, storage)
    {
        VirtualisationFactors = processors;
        Storage = storage;
        Memory = memory;
    }
    #endregion

    #region Methods
    public void AddProccessor(Processor processor, int virtualisationFactor)
    {
        Guard.Against.NegativeOrZero(virtualisationFactor, nameof(virtualisationFactor));
        VirtualisationFactors.Add(processor, virtualisationFactor);
    }

    public void RemoveProcessor(Processor processor)
    {
        VirtualisationFactors.Remove(processor);
    }
    #endregion
}
