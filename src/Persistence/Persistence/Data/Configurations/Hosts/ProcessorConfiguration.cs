﻿using Domain.Hosts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

internal class ProcessorConfiguration : EntityConfiguration<Processor>
{
    public override void Configure(EntityTypeBuilder<Processor> builder)
    {
        base.Configure(builder);
    }
}
