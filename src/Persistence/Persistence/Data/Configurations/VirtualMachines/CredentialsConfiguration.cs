using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class CredentialsConfiguration : EntityConfiguration<Credentials>
{
    public override void Configure(EntityTypeBuilder<Credentials> builder)
    {
        base.Configure(builder);
    }
}
