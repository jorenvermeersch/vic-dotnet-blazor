using Domain.Hosts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.Hosts;

public class VirtualisationFactorConfiguration : EntityConfiguration<VirtualisationFactor>
{
    public override void Configure(EntityTypeBuilder<VirtualisationFactor> builder)
    {
        base.Configure(builder);
    }
}