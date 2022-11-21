using Bogus;
using Domain.Constants;
using Domain.Core;
using Fakers.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Account;
using Shared.customer;
using Shared.Customer;
using Shared.Host;
using Shared.Port;
using Shared.Shared;
using Shared.Specification;
using Shared.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.VirtualMachines;

public class FakeVirtualMachineService : IVirtualMachineService
{
    public readonly List<Domain.Core.VirtualMachine> machines = new();
    public FakeVirtualMachineService()
    {
        VirtualMachineFaker virtualMachineFaker = new VirtualMachineFaker(); 
        machines = virtualMachineFaker.UseSeed(1337).Generate(10);
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(machines.Count);
    }

    //TODO: FAKE VM SERVICE INDEX
    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(/*int offset, int amount*/ VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = machines.AsQueryable();

        response.TotalAmount = query.Count();

        response.VirtualMachines = query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.Fqdn,
            Status = x.Status,
        }).OrderBy(x => x.FQDN).ToList();

        return response;
    }

    //TODO: FAKE VM SERVICE DETAILS
    public Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId)
    {
        return null;
        //return Task.FromResult(machines.Single(x => x.Id == virtualMachineId));
    }

    //TODO: FAKE VM SERVICE USER ID MACHINES
    public async Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = machines.AsQueryable();

        //List<VirtualMachineDto.Details> details = query.Select(x => new VirtualMachineDto.Details
        //{
        //    Id = x.Id,
        //    FQDN = x.Fqdn,
        //    Status = x.Status,
        //    Account = new AccountDto.Index { Id = (int)x.Account.Id, Firstname = x.Account.Firstname, Lastname = x.Account.Lastname, Email = x.Account.Email, IsActive = x.Account.IsActive, Role = x.Account.Role }
        //}).Where(x => x.Account.Id == request.ObjectId).ToList();

        //response.VirtualMachines = details.Select(x => new VirtualMachineDto.Index
        //{
        //    Id = x.Id,
        //    FQDN = x.FQDN, 
        //    Status = x.Status,
        //}).ToList();

        //response.TotalAmount = response.VirtualMachines.Count();


        response.VirtualMachines = (query.Select(x => new VirtualMachineDto.Details
        {
            Id = x.Id,
            FQDN = x.Fqdn,
            Status = x.Status,
            Account = new () { Id = (int)x.Account.Id }
        }).Where(x => x.Account.Id == request.ObjectId).ToList()).Select(x => new VirtualMachineDto.Index { Id = x.Id, FQDN = x.FQDN, Status = x.Status }).ToList();

        response.TotalAmount = response.VirtualMachines.Count();

        return response;
    }

    public Task<VirtualMachineDto.Details> Add(VirtualMachineDto.Create newVM)
    {
        return null;
        //newVM.Id = _virtualMachines.Count + 1;
        //Console.WriteLine(newVM.ApplicationDate);
        //Console.WriteLine(newVM.Specification.Processors);
        //_virtualMachines.Add(newVM);
        //VirtualMachineDto.Details vm = new VirtualMachineDto.Details()
        //{
        //    Id = newVM.Id,
        //    FQDN = newVM.FQDN,
        //    Status = Status.Requested,
        //    Name = newVM.Name,
        //    Template = newVM.Template,
        //    Mode = newVM.Mode,
        //    BackupFrequenty = newVM.BackupFrequenty,
        //    ApplicationDate = newVM.ApplicationDate,
        //    Reason = newVM.Reason,
        //    Ports = newVM.Ports,
        //    Specification = newVM.Specifications,
        //    Credentials = newVM.Credentials,
        //    Host = new(),
        //    Account = new(),
        //    Requester = new(),
        //    TimeSpan = new()
        //    {
        //        StartDate = newVM.StartDate,
        //        EndDate = newVM.EndDate
        //    },
        //    hasVpnConnection = newVM.hasVpnConnection
        //    // add accounts
        //};

        //machines.Add(vm);
        //return Task.FromResult(vm);
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetAllUnfinishedVirtualMachines(int offset)
    {
        return Task.FromResult(machines.Where(vm => vm.Status == Status.InProgress || vm.Status == Status.Requested).Skip(offset).Take(10).Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.Fqdn,
            Status = x.Status,
        }));
    }


    public async Task<VirtualMachineResponse.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = machines.AsQueryable();

        response.TotalAmount = query.Count();

        response.VirtualMachines = query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            FQDN = x.Fqdn,
            Status = x.Status,
        }).ToList();

        return response;
    }

    public Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByUserId(VirtualMachineRequest.GetByObjectId request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VirtualMachineDto.Index>> GetVirtualMachinesByHostId(VirtualMachineRequest.GetByObjectId request)
    {
        throw new NotImplementedException();
    }
}
