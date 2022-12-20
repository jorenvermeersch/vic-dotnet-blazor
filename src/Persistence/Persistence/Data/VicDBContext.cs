using Domain.Accounts;
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
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();
    public DbSet<Port> Ports => Set<Port>();
    public DbSet<Processor> Processors => Set<Processor>();

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

        // You need to define each subclass explicitly if you don't want to create a DbSet for each subclass. 
        // 1. Customer. 
        modelBuilder.Entity<InternalCustomer>();
        modelBuilder.Entity<ExternalCustomer>();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<string>().HaveMaxLength(4_000); // Maximum length that can still be indexed. 
    }
}
