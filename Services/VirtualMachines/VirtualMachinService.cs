using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Service.Accounts;
using Services.Customers;
using Services.Hosts;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

namespace Services.VirtualMachines;

public class VirtualMachinService : IVirtualMachineService
{
    private readonly VicDBContext _dbContext;
    private readonly DbSet<VirtualMachine> _virtualMachines;
    private readonly DbSet<Server> _hosts;
    private readonly DbSet<Port> _ports;
    private readonly DbSet<Customer> _customers;
    private readonly DbSet<Account> _accounts;


    public VirtualMachinService(VicDBContext dbContact)
    {
        _dbContext = dbContact;
        _virtualMachines = _dbContext.VirtualMachines;
        _hosts = _dbContext.Hosts;
        _customers = _dbContext.Customers;
        _accounts = _dbContext.Accounts;
        _ports = _dbContext.Ports;
    }

    private IQueryable<VirtualMachine> GetMachineById(long id) => _virtualMachines
                .AsNoTracking()
                .Where(p => p.Id == id);
    private static CustomerType ReturnsCustomerType(string type)
    {
        CustomerType customerType = new();
        switch (type)
        {
            case "InternalCustomer": customerType = CustomerType.Intern; break;
            case "ExternalCustomer": customerType = CustomerType.Extern; break;
        };
        return customerType;
    }

    private async Task<VirtualMachineArgs> createArgsVirtualMachine(VirtualMachineDto.Mutate model)
    {
        Server host = await _hosts.SingleOrDefaultAsync(x => x.Id == model.HostId)!;
        Customer user = await _customers.SingleOrDefaultAsync(x => x.Id == model.UserId)!;
        Customer requester = await _customers.SingleOrDefaultAsync(x => x.Id == model.RequesterId)!;
        Account account = await _accounts.SingleOrDefaultAsync(x => x.Id == model.AdministratorId)!;

        List<Port> ports = new List<Port>();
        foreach (var port in model.Ports)
        {
            ports.Add(await _ports.SingleOrDefaultAsync(x => x.Number == port)!);
        }

        var args = new VirtualMachineArgs
        {
            Template = model.Template!.Value,
            Mode = model.Mode!.Value,
            Fqdn = model.Fqdn,
            Availabilities = model.Availabilities,
            BackupFrequency = model.BackupFrequency!.Value,
            ApplicationDate = model.ApplicationDate,
            TimeSpan = new Domain.VirtualMachines.TimeSpan(startDate: model.StartDate, endDate: model.EndDate),
            Status = model.Status!.Value,
            Reason = model.Reason,
            Ports = ports, 
            Host = host,
            Credentials = model.Credentials.Select(y => new Credentials(y.Username, y.PasswordHash, y.Role)).ToList(),
            Account = account,
            Requester = requester,
            User = user,
            Name = model.Name,
            HasVpnConnection = model.hasVpnConnection,
            Specifications = new Domain.Common.Specifications(model.Specifications.VirtualProcessors, model.Specifications.Memory, model.Specifications.Storage),
        };
        return args;
    }

    public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        VirtualMachineResponse.Create response = new();
        var machine = _virtualMachines.Add(new VirtualMachine(await createArgsVirtualMachine(request.VirtualMachine)));
        await _dbContext.SaveChangesAsync();
        response.MachineId = machine.Entity.Id;
        return response;
    }

    public async Task DeleteAsync(VirtualMachineRequest.Delete request)
    {
        _virtualMachines.RemoveIf(vm => vm.Id == request.MachineId);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
    {
        VirtualMachineResponse.Edit response = new();
        var machine = await GetMachineById(request.MachineId).SingleOrDefaultAsync();

        var model = request.VirtualMachine;

        var args = await createArgsVirtualMachine(model);

        if(machine is not null)
        {
            machine.Template = args.Template;
            machine.Mode = args.Mode;
            machine.Fqdn = args.Fqdn;
            machine.Availabilities = args.Availabilities;
            machine.BackupFrequency = args.BackupFrequency;
            machine.ApplicationDate = args.ApplicationDate;
            machine.Specifications = args.Specifications;
            machine.TimeSpan = args.TimeSpan;
            machine.Status = args.Status;
            machine.Reason = args.Reason;
            machine.Ports = args.Ports;
            machine.Host = args.Host;
            machine.Credentials = args.Credentials;
            machine.Account = args.Account;
            machine.Requester = args.Requester;
            machine.User = args.User;
            machine.HasVpnConnection = args.HasVpnConnection;
            machine.Credentials = args.Credentials;

            _dbContext.Entry(machine).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            response.MachineId = machine.Id;
        }

        return response;
    }

    public async Task<VirtualMachineResponse.GetAllDetails> GetAllDetailsAsync(VirtualMachineRequest.GetAllDetails request)
    {
        VirtualMachineResponse.GetAllDetails response = new();
        var query = _virtualMachines.AsQueryable().AsNoTracking();

        response.VirtualMachines = await query.Select(vm => new VirtualMachineDto.Detail
        {
            Id = vm.Id,
            Fqdn = vm.Fqdn,
            Status = vm.Status,
            Name = vm.Name,
            Template = vm.Template,
            Mode = vm.Mode,
            Availabilities = vm.Availabilities.Select(x => x.ToString()).ToList() ?? new List<string>(),
            BackupFrequenty = vm.BackupFrequency,
            ApplicationDate = vm.ApplicationDate,
            TimeSpan = new TimeSpanDto() { StartDate = vm.TimeSpan.StartDate, EndDate = vm.TimeSpan.EndDate },
            Reason = vm.Reason,
            Ports = vm.Ports.Select(y => new PortDto { Number = y.Number, Service = y.Service }).ToList(),
            Specification = new SpecificationsDto() { Memory = vm.Specifications.Memory, Storage = vm.Specifications.Storage, VirtualProcessors = vm.Specifications.Processors },
            Host = new HostDto.Index() { Id = vm.Host.Id, Name = vm.Host.Name },
            Credentials = vm.Credentials.Select(y => new CredentialsDto { Username = y.Username, Role = y.Role, PasswordHash = y.PasswordHash }).ToList(),
            Account = new AccountDto.Index() { Id = vm.Account.Id, Email = vm.Account.Email, Firstname = vm.Account.Firstname, Lastname = vm.Account.Lastname, IsActive = vm.Account.IsActive, Role = vm.Account.Role },
            Requester = new CustomerDto.Index() { Id = vm.Requester.Id, Name = (vm.Requester.ContactPerson.Firstname + " " + vm.Requester.ContactPerson.Lastname), Email = vm.Requester.ContactPerson.Email, CustomerType = ReturnsCustomerType(vm.Requester.GetType().ToString()) },
            User = new CustomerDto.Index() { Id = vm.User.Id, Name = (vm.User.ContactPerson.Firstname + " " + vm.User.ContactPerson.Lastname), Email = vm.User.ContactPerson.Email, CustomerType = ReturnsCustomerType(vm.User.GetType().ToString()) },
            hasVpnConnection = vm.HasVpnConnection
        }).ToListAsync();

        response.TotalAmount = query.Count();

        return response;
    }

    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        VirtualMachineResponse.GetDetail response = new()
        {
            VirtualMachine = await GetMachineById(request.MachineId).Select(x => new VirtualMachineDto.Detail
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
                Specification = new SpecificationsDto() { Memory = x.Specifications.Memory, Storage = x.Specifications.Storage, VirtualProcessors = x.Specifications.Processors },
                Host = new HostDto.Index() { Id = x.Host.Id, Name = x.Host.Name },
                Credentials = x.Credentials.Select(y => new CredentialsDto { Username = y.Username, Role = y.Role, PasswordHash = y.PasswordHash }).ToList(),
                Account = new AccountDto.Index() { Id = x.Account.Id, Email = x.Account.Email, Firstname = x.Account.Firstname, Lastname = x.Account.Lastname, IsActive = x.Account.IsActive, Role = x.Account.Role },
                Requester = new CustomerDto.Index() { Id = x.Requester.Id, Name = (x.Requester.ContactPerson.Firstname + " " + x.Requester.ContactPerson.Lastname), Email = x.Requester.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.Requester.GetType().ToString()) },
                User = new CustomerDto.Index() { Id = x.User.Id, Name = (x.User.ContactPerson.Firstname + " " + x.User.ContactPerson.Lastname), Email = x.User.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.User.GetType().ToString()) },
                hasVpnConnection = x.HasVpnConnection
            })!.SingleOrDefaultAsync(),
        };  
        return response;
    }

    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = _virtualMachines.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(x => x.Fqdn.Contains(request.SearchTerm));
        if (request.IsUnfinished)
        {
            query = query.Where(x => x.Status == Status.InProgress || x.Status == Status.Requested);
        }
        response.TotalAmount = query.Count();

        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        query.OrderBy(x => x.Name);
        response.VirtualMachines = await query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status

        }).ToListAsync();

        return response;
    }
}
