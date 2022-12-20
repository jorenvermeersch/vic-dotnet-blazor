using Domain.Hosts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class ServerHistoryConfiguration : EntityConfiguration<ServerHistory>
{
    private readonly string suffix = "Used";

    public override void Configure(EntityTypeBuilder<ServerHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable("History");

        builder.OwnsOne(x => x.Specifications, specifications =>
        {
            specifications.Property(x => x.Processors).HasColumnName(nameof(ServerHistory.Specifications.Processors));
            specifications.Property(x => x.Memory).HasColumnName(nameof(ServerHistory.Specifications.Memory));
            specifications.Property(x => x.Storage).HasColumnName(nameof(ServerHistory.Specifications.Storage));
        });

        builder.OwnsOne(x => x.SpecificationsUsed, specifications =>
        {
            specifications.Property(x => x.Processors).HasColumnName($"{nameof(ServerHistory.SpecificationsUsed.Processors)}{suffix}");
            specifications.Property(x => x.Memory).HasColumnName($"{nameof(ServerHistory.SpecificationsUsed.Memory)}{suffix}");
            specifications.Property(x => x.Storage).HasColumnName($"{nameof(ServerHistory.SpecificationsUsed.Storage)}{suffix}");
        });
    }
}
