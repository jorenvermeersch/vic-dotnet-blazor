using Domain.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.Customers;

public class ContactPersonConfiguration : EntityConfiguration<ContactPerson>
{
    public override void Configure(EntityTypeBuilder<ContactPerson> builder)
    {
        base.Configure(builder);
    }
}
