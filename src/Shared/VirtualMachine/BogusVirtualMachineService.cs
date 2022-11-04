using Bogus;
using Shared.Account;
using Shared.customer;
using Shared.Customer;
using Shared.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public class BogusVirtualMachineService : IVirtualMachineService
{
    BogusCustomerService BogusCustomerService { get; set; } = new BogusCustomerService();
    BogusAccountService BogusAccountService { get; set; } = new BogusAccountService();
    BogusHostService BogusHostService { get; set; } = new BogusHostService();
    private readonly List<VirtualMachineDto.Details> _virtualMachines = new();
    
    public BogusVirtualMachineService()
    {
        var vmId = 0;

        
        var customers = BogusCustomerService.customers.Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = x.ContactPerson.Firstname + " " + x.ContactPerson.Lastname,
            Email = x.ContactPerson.Email
        }).ToArray();
        var accounts = BogusAccountService.accounts.Select(x => new AccountDto.Index
        {
            Id = x.Id,
            Name = x.Firstname + " " + x.Lastname,
            Email = x.Email
        }).ToArray();
        var hosts = BogusHostService.hosts;


        var portFaker = new Faker<PortDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Number, f => f.Internet.Port())
            .RuleFor(x => x.Service, f => f.Internet.Protocol());

        var credentialFaker = new Faker<CredentialDto>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Username, f => f.Internet.UserName())
            .RuleFor(x => x.Role, f => f.PickRandom(new[] {"Admin", "User", "Observer" }));


        var virtualMachineFaker = new Faker<VirtualMachineDto.Details>("nl")
            .UseSeed(1337)
            .RuleFor(x => x.Id, _ => vmId++)
            .RuleFor(x => x.FQDN, f => f.Internet.DomainName())
            .RuleFor(x => x.Name, f => f.Internet.DomainWord())
            .RuleFor(x => x.Template, f => f.PickRandom(new[] { "", "AI" }))
            .RuleFor(x => x.Mode, f => f.PickRandom(new[] { "SAAS", "PAAS", "IAAS" }))
            .RuleFor(x => x.BackupFrequenty, f => f.PickRandom(new[] { "Dagelijks", "Wekelijks", "Maandelijks" }))
            .RuleFor(x => x.Availabilities, f => Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(new[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" })).ToList())
            .RuleFor(x => x.Status, f => f.PickRandom(new[] { "aanvraag", "in verwerking", "afgehandeld" }))
            .RuleFor(x => x.Reason, f => f.PickRandom(new[] { "Bachelor proef", "AI trainen", "Database draaien" }))
            .RuleFor(x => x.Mode, f => f.PickRandom(new[] { "SAAS", "PAAS", "IAAS" }))
            .RuleFor(x => x.ApplicationDate, f => f.Date.Past())
            .RuleFor(x => x.Storage, f => f.Random.Number(2,1000))
            .RuleFor(x => x.Memory, f => f.Random.Number(2, 128))
            .RuleFor(x => x.Processors, f => f.Random.Number(2, 128))
            .RuleFor(x => x.StartDate, (f, u) => f.Date.Between(u.ApplicationDate, new DateTime(2023,01,30)))
            .RuleFor(x => x.EndDate, (f,u) => f.Date.Between(u.StartDate, new DateTime(2023, 01, 30)))
            .RuleFor(x => x.User, f => f.PickRandom(customers))
            .RuleFor(x => x.Requester, f => f.PickRandom(customers))
            .RuleFor(x => x.Account, f => f.PickRandom(accounts))
            .RuleFor(x=> x.Ports, f=> Enumerable.Range(1, f.Random.Int(1, 2)).Select(x => f.PickRandom(portFaker.Generate(5))).ToList())
            .RuleFor(x => x.Credentials, f => Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(credentialFaker.Generate(25))).ToList())
            .RuleFor(x=>x.Host, f=>f.PickRandom(hosts));

        //server-todo
        //credentials-todo

        _virtualMachines = virtualMachineFaker.Generate(25);

    }
    public Task<int> GetCount()
    {
        return Task.FromResult(_virtualMachines.Count);
    }

    public Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId)
    {
        return Task.FromResult(_virtualMachines.Single(x => x.Id == virtualMachineId));
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetIndexAsync(int offset)
    {
        return Task.FromResult(_virtualMachines.Skip(offset).Take(20).Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.FQDN,
            IsActive = x.EndDate <= DateTime.Now && x.StartDate >= DateTime.Now, //Werkt niet
        })) ;
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId)
    {
        return Task.FromResult(_virtualMachines.Where(vm=>vm.User.Id==userId).Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.FQDN,
            IsActive = x.EndDate <= DateTime.Now && x.StartDate >= DateTime.Now, //Werkt niet
        }));
    }
}
