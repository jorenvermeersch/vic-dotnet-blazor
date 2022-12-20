using Domain.Hosts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.Hosts;

public class ServerProcessorConfiguration : EntityConfiguration<ServerProcessor>
{
    public override void Configure(EntityTypeBuilder<ServerProcessor> builder)
    {
        base.Configure(builder);
    }
}
