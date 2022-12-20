using Domain.Hosts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class ServerConfiguration : EntityConfiguration<Server>
{
    public override void Configure(EntityTypeBuilder<Server> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.RemainingResources);

        builder.OwnsOne(x => x.Specifications, specifications =>
        {
            specifications.Ignore(x => x.VirtualisationFactors); // Workaround. 
            specifications.Ignore(x => x.Processors); // Calculated property. 
            specifications.Property(x => x.Memory).HasColumnName(nameof(Server.Specifications.Memory));
            specifications.Property(x => x.Storage).HasColumnName(nameof(Server.Specifications.Storage));
        });

    }
}
