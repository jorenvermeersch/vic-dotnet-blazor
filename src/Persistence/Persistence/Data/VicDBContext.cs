using Domain.Accounts;
using Domain.Common;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Triggers;

namespace Persistence.Data;

public class VicDbContext : DbContext
{
    public VicDbContext(DbContextOptions<VicDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // More detailed errors for development. 
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();

        // Triggers. 
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VicDbContext).Assembly);

        //modelBuilder.Ignore<Account>();
        modelBuilder.Ignore<Specifications>();
        modelBuilder.Ignore<ContactPerson>();
        modelBuilder.Ignore<Customer>();
        modelBuilder.Ignore<InternalCustomer>();
        modelBuilder.Ignore<ExternalCustomer>();
        modelBuilder.Ignore<Machine>();
        modelBuilder.Ignore<Server>();
        modelBuilder.Ignore<HostSpecifications>();
        modelBuilder.Ignore<Processor>();
        modelBuilder.Ignore<VirtualMachine>();
        modelBuilder.Ignore<Credentials>();
        modelBuilder.Ignore<Port>();
        modelBuilder.Ignore<Domain.VirtualMachines.TimeSpan>();
        modelBuilder.Ignore<VirtualMachine>();
    }

}
