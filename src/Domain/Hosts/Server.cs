﻿using Domain.Common;
using Domain.VirtualMachines;

namespace Domain.Hosts;

[ToString]
public class Server : Host<VirtualMachine>
{
    #region Constructors
    public Server(string name, HostSpecifications resources, ISet<VirtualMachine> virtualMachines)
        : base(name, resources, virtualMachines) { }
    #endregion
}