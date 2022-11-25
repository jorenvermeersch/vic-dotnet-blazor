using Domain.Constants;
using Domain.VirtualMachines;
using Fakers.VirtualMachines;
using Shared.VirtualMachine;

namespace Service.VirtualMachines;

public class FakeVirtualMachineService : IVirtualMachineService
{
    public readonly List<VirtualMachine> machines = new();
    public FakeVirtualMachineService()
    {
        VirtualMachineFaker virtualMachineFaker = new();
        machines = virtualMachineFaker.UseSeed(1337).Generate(10);
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(machines.Count);
    }

    //TODO: FAKE VM SERVICE INDEX
    public async Task<VirtualMachineResult.GetIndex> GetIndexAsync(/*int offset, int amount*/ VirtualMachineRequest.Index request)
    {
        VirtualMachineResult.GetIndex response = new();
        var query = machines.AsQueryable();

        response.TotalAmount = query.Count();

        response.VirtualMachines = query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status,
        }).OrderBy(x => x.Fqdn).ToList();

        return response;
    }

    //TODO: FAKE VM SERVICE DETAILS
    public Task<VirtualMachineDto.Details> GetDetailAsync(long virtualMachineId)
    {
        return null;
        //return Task.FromResult(machines.Single(x => x.Id == virtualMachineId));
    }

    //TODO: FAKE VM SERVICE USER ID MACHINES
    public async Task<VirtualMachineResult.GetIndex> GetVirtualMachinesByAccountId(VirtualMachineRequest.GetByObjectId request)
    {
        VirtualMachineResult.GetIndex response = new();
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
            Fqdn = x.Fqdn,
            Status = x.Status,
            Account = new() { Id = (int)x.Account.Id }
        }).Where(x => x.Account.Id == request.ObjectId).ToList()).Select(x => new VirtualMachineDto.Index { Id = x.Id, Fqdn = x.Fqdn, Status = x.Status }).ToList();

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
            Fqdn = x.Fqdn,
            Status = x.Status,
        }));
    }


    public async Task<VirtualMachineResult.GetIndex> GetAllUnfinishedVirtualMachines(VirtualMachineRequest.Index request)
    {
        VirtualMachineResult.GetIndex response = new();
        var query = machines.AsQueryable();

        response.TotalAmount = query.Count();

        response.VirtualMachines = query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status,
        }).ToList();

        return response;
    }

    public Task<VirtualMachineResult.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
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
