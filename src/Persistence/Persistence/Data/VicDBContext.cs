using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Core;
using System.Reflection;

namespace Persistence.Data;

public class VicDBContext : DbContext
{
    public VicDBContext(DbContextOptions<VicDBContext> options)
            : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
