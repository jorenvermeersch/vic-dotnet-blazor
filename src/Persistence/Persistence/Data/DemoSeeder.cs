﻿using Domain.Accounts;
using Domain.Constants;
using Domain.Hosts;
using Domain.VirtualMachines;

namespace Persistence.Data;

public class DemoSeeder : IDatabaseSeeder
{
    private readonly VicDbContext dbContext;

    private bool seed = true;
    public DemoSeeder(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        bool isNewDatabase = dbContext.Database.EnsureCreated();

        if (isNewDatabase)
        {
            SeedAccounts();
            SeedPorts();
            SeedProcessors();
        }
    }

    private void SeedAccounts()
    {
        dbContext.Accounts.AddRange(
           new Account("Joren", "Vermeersch", "joren.vermeersch@student.hogent.be", Role.Admin, "oE321^W0!%*d", "DIT", "Toegepaste Informatica", true),
           new Account("Robin", "Vermeir", "robin.vermeir@student.hogent.be", Role.Observer, "n83NNH%2VTfQ", "DIT", "Toegepaste Informatica", true),
           new Account("Angela", "Degryse", "angela.degryse@student.hogent.be", Role.Master, "Y1yG9tv$48YB", "DIT", "Toegepaste Informatica", true),
           new Account("Kerem", "Yilmaz", "kerem.yimaz@student.hogent.be", Role.Observer, "Y1yG9tv$48YE", "DIT", "Toegepaste Informatica", true)
        );
        dbContext.SaveChanges();
    }

    private void SeedPorts()
    {
        dbContext.Ports.AddRange(
           new List<Port>
           {
                 new Port(number: 443, service: "HTTPS"),
                 new Port(number: 80, service: "HTTP"),
                 new Port(number: 22, "SSH")
           }
        );
        dbContext.SaveChanges();
    }
    private void SeedProcessors()
    {
        dbContext.Processors.AddRange(
          new Processor("i9-11900H", 8, 16),
          new Processor("i9-11980HK", 8, 16),
          new Processor("i9-11950H", 8, 16),
          new Processor("i9-11900F", 8, 16),
          new Processor("i9-11900K", 8, 16),
          new Processor("i7-11850HE", 8, 16),
          new Processor("i7-11600H", 6, 12),
          new Processor("i7-11390H", 4, 8),
          new Processor("i7-1195G7", 4, 8),
          new Processor("i7-1195G8", 4, 12),
          new Processor("i5-11500HE", 6, 12),
          new Processor("i5-11320H", 4, 8),
          new Processor("i5-1155G7", 4, 8),
          new Processor("i5-1155G8", 4, 12),
          new Processor("i5-11260H", 6, 12)
        );
        dbContext.SaveChanges();
    }
}
