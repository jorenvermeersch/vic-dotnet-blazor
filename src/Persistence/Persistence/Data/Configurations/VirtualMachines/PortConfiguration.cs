using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class PortConfiguration : EntityConfiguration<Port>
{
    public override void Configure(EntityTypeBuilder<Port> builder)
    {
        base.Configure(builder);

        // Many-to-many relationship only defined on one side in Domain.  
        builder.HasMany<VirtualMachine>().WithMany(x => x.Ports);
    }
}
