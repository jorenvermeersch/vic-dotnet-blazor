using Domain.Accounts;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

namespace Persistence.Data;

public class VicDBContext : DbContext
{
    public VicDBContext(DbContextOptions<VicDBContext> options)
            : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<InternalCustomer> InternalCustomers => Set<InternalCustomer>();
    public DbSet<ExternalCustomer> ExternalCustomers => Set<ExternalCustomer>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();
    public DbSet<Server> Hosts => Set<Server>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .Entity<Customer>()
        .Property(e => e.ContactPerson)
        .IsRequired(false)
        .HasConversion(v => JsonConvert.SerializeObject(v),
                     v => JsonConvert.DeserializeObject<ContactPerson>(v));

        modelBuilder
        .Entity<Customer>()
        .Property(e => e.BackupContactPerson)
        .IsRequired(false)
        .HasConversion(v => JsonConvert.SerializeObject(v),
                     v => JsonConvert.DeserializeObject<ContactPerson>(v));
        //modelBuilder.Entity<Customer>();
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
