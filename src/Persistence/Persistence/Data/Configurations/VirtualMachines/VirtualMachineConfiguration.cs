using Domain.Common;
using Domain.Constants;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.VirtualMachines;

public class VirtualMachineConfiguration : EntityConfiguration<VirtualMachine>
{
    public override void Configure(EntityTypeBuilder<VirtualMachine> builder)
    {
        base.Configure(builder);

        // ValueObject: Specifications. 
        builder.OwnsOne(x => x.Specifications, specifications =>
        {
            specifications.Property(x => x.Processors).HasColumnName(nameof(Specifications.Processors));
            specifications.Property(x => x.Memory).HasColumnName(nameof(Specifications.Memory));
            specifications.Property(x => x.Storage).HasColumnName(nameof(Specifications.Storage));
        });

        // ValueObject: TimeSpan. 
        builder.OwnsOne(x => x.TimeSpan, timeSpan =>
        {
            timeSpan.Property(x => x.StartDate).HasColumnName(nameof(Domain.VirtualMachines.TimeSpan.StartDate));
            timeSpan.Property(x => x.EndDate).HasColumnName(nameof(Domain.VirtualMachines.TimeSpan.EndDate));
        });

        // Relationship with User and Requester. 
        // User and Requester will never be deleted. They will simply be deactivated. 
        builder.HasOne(x => x.Requester).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.NoAction);

        // Availability. 
        var valueComparer = new ValueComparer<IList<Availability>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList());

        builder.Property(x => x.Availabilities)
        .HasConversion(
            days => string.Join(',', days),
            value => ParseDays(value))
        .Metadata.SetValueComparer(valueComparer);
    }

    private List<Availability> ParseDays(string value)
    {
        List<string> unparsedDays = value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        List<Availability> parsedDays = unparsedDays.Select(value => (Availability)Enum.Parse(typeof(Availability), value)).ToList();
        return parsedDays;
    }
}
