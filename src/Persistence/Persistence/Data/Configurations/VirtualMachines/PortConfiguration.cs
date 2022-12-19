﻿using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class PortConfiguration : EntityConfiguration<Port>
{
    public override void Configure(EntityTypeBuilder<Port> builder)
    {
        base.Configure(builder);
    }
}