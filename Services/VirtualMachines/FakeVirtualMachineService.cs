using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Domain.Exceptions;
using Domain.Hosts;
using Domain.VirtualMachines;
using Service.Accounts;
using Services.Customers;
using Services.FakeInitializer;
using Services.Hosts;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

namespace Service.VirtualMachines;

public class FakeVirtualMachineService : IVirtualMachineService
{
    private static readonly List<VirtualMachine> machines = new();
    static FakeVirtualMachineService()
    {
        machines = FakeInitializerService.FakeVirtualMachines ?? new List<VirtualMachine>();
    }

    // Helper Function
    private CustomerType ReturnsCustomerType(string type)
    {
        CustomerType customerType = new();
        switch (type)
        {
            case "InternalCustomer": customerType = CustomerType.Intern; break;
            case "ExternalCustomer": customerType = CustomerType.Extern; break;
        };
        return customerType;
    }

    private VirtualMachineArgs createArgsVirtualMachine(VirtualMachineDto.Mutate model)
    {
        Server host = FakeHostService.Hosts.Where(x => x.Id == model.HostId).SingleOrDefault();
        Customer user = FakeCustomerService.Customers.Where(x => x.Id == model.UserId).SingleOrDefault();
        Customer requester = FakeCustomerService.Customers.Where(x => x.Id == model.RequesterId).SingleOrDefault();
        Account account = FakeAccountService.Accounts.Where(x => x.Id == model.AdministratorId).SingleOrDefault();

        var args = new VirtualMachineArgs
        {
            Template = model.Template,
            Mode = model.Mode,
            Fqdn = model.Fqdn,
            Availabilities = model.Availabilities,
            BackupFrequency = model.BackupFrequency,
            ApplicationDate = model.ApplicationDate,
            TimeSpan = new Domain.VirtualMachines.TimeSpan(startDate: model.StartDate, endDate: model.EndDate),
            Status = model.Status,
            Reason = model.Reason,
            Ports = /*model.Ports.Select(x => new Port(x, x.ToString())).ToList()*/new List<Port>(),
            Host = host,
            Credentials = model.Credentials.Select(y => new Credentials(y.Username, y.PasswordHash, y.Role)).ToList(),
            Account = account,
            Requester = requester,
            User = user,
            Name = model.Name,
            HasVpnConnection = model.hasVpnConnection,
            Specifications = new Domain.Common.Specifications(memory: model.Specifications.Memory, storage: model.Specifications.Storage)
        };
        return args;
    }

    public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        VirtualMachineResponse.Create response = new();
        var model = request.VirtualMachine;
        var args = createArgsVirtualMachine(model);
        var machine = new VirtualMachine(args) { Id = machines.Max(x => x.Id) + 1 };
        machines.Add(machine);
        response.MachineId = machine.Id;

        return response;
    }
    public async Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
    {
        VirtualMachineResponse.Edit response = new();
        var machine = machines.SingleOrDefault(x => x.Id == request.MachineId);

        var model = request.VirtualMachine;

        var args = createArgsVirtualMachine(model);

        machine.Template = args.Template;
        machine.Mode = args.Mode;
        machine.Fqdn = args.Fqdn;
        machine.Availabilities = args.Availabilities;
        machine.BackupFrequency = args.BackupFrequency;
        machine.ApplicationDate = args.ApplicationDate;
        machine.TimeSpan = args.TimeSpan;
        machine.Status = args.Status;
        machine.Reason = args.Reason;
        machine.Ports = args.Ports;
        machine.Host = args.Host;
        machine.Credentials = args.Credentials;
        machine.Account = args.Account;
        machine.Requester = args.Requester;
        machine.User = args.User;

        response.MachineId = machine.Id;
        return response;
    }



    public async Task DeleteAsync(VirtualMachineRequest.Delete request)
    {
        //machines.RemoveAll(x => x.Id == request.MachineId);

        VirtualMachine? machine = machines.SingleOrDefault(x => x.Id == request.MachineId);

        if (machine is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), request.MachineId);

        machines.Remove(machine);
    }


    //TODO: host returns null because faker is not set properly
    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        VirtualMachineResponse.GetDetail response = new();
        var query = machines.AsQueryable();

        response.VirtualMachine = query.Select(x => new VirtualMachineDto.Detail
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status,
            Name = x.Name,
            Template = x.Template,
            Mode = x.Mode,
            Availabilities = x.Availabilities.Select(x => x.ToString()).ToList() ?? new List<string>(),
            BackupFrequenty = x.BackupFrequency,
            ApplicationDate = x.ApplicationDate,
            TimeSpan = new TimeSpanDto() { StartDate = x.TimeSpan.StartDate, EndDate = x.TimeSpan.EndDate },
            Reason = x.Reason,
            Ports = x.Ports.Select(y => new PortDto { Number = y.Number, Service = y.Service }).ToList(),
            // specifications may be wrong
            Specification = new SpecificationsDto() { Memory = x.Specifications.Memory, Storage = x.Specifications.Storage, VirtualProcessors = x.Specifications.Processors },
            Host = new HostDto.Index() { Id = x.Host.Id, Name = x.Host.Name },
            Credentials = x.Credentials.Select(y => new CredentialsDto { Username = y.Username, Role = y.Role, PasswordHash = y.PasswordHash }).ToList(),
            Account = new AccountDto.Index() { Id = x.Account.Id, Email = x.Account.Email, Firstname = x.Account.Firstname, Lastname = x.Account.Lastname, IsActive = x.Account.IsActive, Role = x.Account.Role },
            Requester = new CustomerDto.Index() { Id = x.Requester.Id, Name = (x.Requester.ContactPerson.Firstname + " " + x.Requester.ContactPerson.Lastname), Email = x.Requester.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.Requester.GetType().ToString()) },
            User = new CustomerDto.Index() { Id = x.User.Id, Name = (x.User.ContactPerson.Firstname + " " + x.User.ContactPerson.Lastname), Email = x.User.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.User.GetType().ToString()) },
            hasVpnConnection = x.HasVpnConnection
        }).SingleOrDefault(x => x.Id == request.MachineId) ?? new VirtualMachineDto.Detail();
        return response;
    }


    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = machines.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(x => x.Fqdn.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        if (request.IsUnfinished)
        {
            query = query.Where(x => x.Status == Status.InProgress || x.Status == Status.Requested);
        }
        response.TotalAmount = query.Count();

        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        query.OrderBy(x => x.Name);
        response.VirtualMachines = query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status

        }).ToList();
        return response;
    }

    public Task<VirtualMachineResponse.GetIndex> GetUnfinishedAsync(VirtualMachineRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
