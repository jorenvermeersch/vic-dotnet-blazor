using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public class FakeVirtualMachineService:IVirtualMachineService
{
    private readonly List<VirtualMachineDto.Details> _virtualMachines = new();

    public FakeVirtualMachineService()
    {
        /*
        _virtualMachines.Add(new()
        {
            Id = 1,
            Name = "EU-SERV-1",
            FQDN = "devops-proj1.vichogent.be",
            Template = "AI",
            Mode = "PAAS",
            Availabilities = new List<string>() { "Maandag", "Dinsdag", "Woensdag" },
            BackupFrequenty = "Dagelijks",
            ApplicationDate = DateTime.Now,
            StartDate = new DateTime(2022,11,1),
            EndDate = new DateTime(2022, 12, 1)  ,         
            Status = "Afgewerkt",
            Reason = "Bachelor proef",
            Storage = 500,
            Memory=24,
            Processors=5,
            Account = new()
            {
                Id = 1,
                Name = "William De Groot",
                Email = "william.degroot@hogent.be"
            },
            Requester = new()
            {
                Id = 3,
                Name = "Kerem Yilmaz",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            User = new()
            {
                Id = 3,
                Name = "Kerem Yilmaz",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            Ports = new List<PortDto>
            {
                new PortDto()
                {
                    Number=22,
                    Service = "SSH"
                }
            }
        });
        _virtualMachines.Add(new()
        {
            Id = 2,
            Name = "EU-SERV-3",
            FQDN = "vm2-hogent.be",
            Template = "/",
            Mode = "SAAS",
            Availabilities = new List<string>() { "Dinsdag", "Woensdag" },
            BackupFrequenty = "Wekelijks",
            ApplicationDate = new DateTime(2022,9,20),
            StartDate = new DateTime(2022, 9, 30),
            EndDate = new DateTime(2022, 10, 25),
            Status = "Afgewerkt",
            Reason = "Bachelor proef",
            Storage = 1000,
            Memory = 36,
            Processors = 10,
            Account = new()
            {
                Id = 2,
                Name = "Janine De Witte",
                Email = "janine.dewitte@hogent.be"
            },
            Requester = new()
            {
                Id = 3,
                Name = "Kerem Yilmaz",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            User = new()
            {
                Id = 3,
                Name = "Kerem Yilmaz",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            Ports= new List<PortDto> 
            { 
                new PortDto()
                { 
                    Number=22,
                    Service = "SSH"
                },
                new PortDto()
                {
                    Number=80,
                    Service = "Http"
                }
            }
            
        });
        _virtualMachines.Add(new()
        {
            Id = 3,
            Name = "EU-SERV-5",
            FQDN = "vm3-hogent.be",
            Template = "/",
            Mode = "SAAS",
            Availabilities = new List<string>() { "Woensdag" },
            BackupFrequenty = "Maandelijks",
            ApplicationDate = new DateTime(2022, 10, 20),
            StartDate = new DateTime(2022, 10, 25),
            EndDate = new DateTime(2022, 11, 25),
            Status = "Afgewerkt",
            Reason = "Database draaien",
            Storage = 255,
            Memory = 16,
            Processors = 4,
            Account = new()
            {
                Id = 3,
                Name = "Jack Sparrow",
                Email = "jack.sparrow@hogent.be"
            },
            Requester = new()
            {
                Id = 3,
                Name = "Robin Vermeire",
                Email = "robin.vermeire@student.hogent.be"
            },
            User = new()
            {
                Id = 3,
                Name = "Kerem Yilmaz",
                Email = "kerem.yilmaz@student.hogent.be"
            },
            Ports = new List<PortDto>
            {
                        new PortDto()
                        {
                            Number = 22,
                            Service = "SSH"
                        },
                        new PortDto()
                        {
                            Number = 80,
                            Service = "Http"
                        }
                    }

        }) ;
        */
    }

    public Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Details newVM)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetAllUnfinishedVirtualMachines(int offset)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(_virtualMachines.Count);
    }

    public Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId)
    {
        return Task.FromResult(_virtualMachines.Single(x => x.Id == virtualMachineId));
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetIndexAsync(int offset, int amount)
    {
        return Task.FromResult(_virtualMachines.Skip(offset).Take(20).Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.FQDN,
            //IsActive = x.EndDate<=DateTime.Now && x.StartDate>=DateTime.Now, //Werkt niet
        }));
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(long userId)
    {
        return Task.FromResult(_virtualMachines.Where(vm => vm.User.Id == userId).Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.FQDN,
            //IsActive = x.EndDate <= DateTime.Now && x.StartDate >= DateTime.Now, //Werkt niet
        }));
    }
}
