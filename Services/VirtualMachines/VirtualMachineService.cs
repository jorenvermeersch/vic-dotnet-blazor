using Domain.Accounts;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Exceptions;
using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

namespace Services.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly VicDbContext dbContext;
    private readonly DbSet<VirtualMachine> virtualMachines;
    private readonly DbSet<Server> hosts;
    private readonly DbSet<Port> ports;
    private readonly DbSet<Customer> customers;
    private readonly DbSet<Account> accounts;


    public VirtualMachineService(VicDbContext dbContact)
    {
        dbContext = dbContact;
        virtualMachines = dbContext.VirtualMachines;
        hosts = dbContext.Hosts;
        customers = dbContext.Customers;
        accounts = dbContext.Accounts;
        ports = dbContext.Ports;
    }

    private IQueryable<VirtualMachine> GetMachineById(long id)
    {
        return virtualMachines
                .AsNoTracking()
                .Where(p => p.Id == id);
    }

    private static CustomerType GetCustomerType(Customer customer)
    {
        return customer is InternalCustomer ? CustomerType.Intern : CustomerType.Extern;
    }

    private static string GetFullName(ContactPerson contactPerson)
    {
        return $"{contactPerson.Firstname} {contactPerson.Lastname}";
    }

    private async Task<VirtualMachineArgs> CreateArgsVirtualMachine(VirtualMachineDto.Mutate model)
    {
        // Fetch host. 
        Server? host = await hosts.SingleOrDefaultAsync(x => x.Id == model.HostId);
        if (host is null) throw new EntityNotFoundException(nameof(Server), model.HostId);

        // Fetch user, requester and administrator. 
        Customer? user = await customers.SingleOrDefaultAsync(x => x.Id == model.UserId);
        if (user is null) throw new EntityNotFoundException(nameof(VirtualMachine.User), model.UserId);

        Customer? requester = await customers.SingleOrDefaultAsync(x => x.Id == model.RequesterId);
        if (requester is null) throw new EntityNotFoundException(nameof(VirtualMachine.Requester), model.RequesterId);

        Account? account = await accounts.SingleOrDefaultAsync(x => x.Id == model.AdministratorId);
        if (account is null) throw new EntityNotFoundException(nameof(VirtualMachine.Account), model.AdministratorId);

        // Fetch ports. 
        List<Port> ports = new();

        foreach (var portNumber in model.Ports)
        {
            Port? port = await this.ports.SingleOrDefaultAsync(x => x.Number == portNumber);
            if (port is null) throw new EntityNotFoundException(nameof(Port), portNumber);
            ports.Add(port);
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
            Credentials = model.Credentials.Select(credential => new Credentials(credential.Username, credential.PasswordHash, credential.Role)).ToList(),
            Account = account,
            Requester = requester,
            User = user,
            Name = model.Name,
            HasVpnConnection = model.hasVpnConnection,
            Specifications = new Specifications(model.Specifications.VirtualProcessors, model.Specifications.Memory, model.Specifications.Storage),
        };

        return args;
    }

    public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        VirtualMachineResponse.Create response = new();
        VirtualMachineArgs arguments = await CreateArgsVirtualMachine(request.VirtualMachine);
        var machine = virtualMachines.Add(new VirtualMachine(arguments));

        await dbContext.SaveChangesAsync();
        response.MachineId = machine.Entity.Id;

        return response;
    }

    public Task DeleteAsync(VirtualMachineRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    private static VirtualMachineDto.Detail ToVirtualMachineDetail(VirtualMachine machine)
    {
        return new VirtualMachineDto.Detail
        {
            Id = machine.Id,
            Fqdn = machine.Fqdn,
            Status = machine.Status,
            Name = machine.Name,
            Template = machine.Template,
            Mode = machine.Mode,
            Availabilities = machine.Availabilities.Select(x => x.ToString()).ToList()!,
            BackupFrequenty = machine.BackupFrequency,
            ApplicationDate = machine.ApplicationDate,
            TimeSpan = new TimeSpanDto()
            {
                StartDate = machine.TimeSpan.StartDate,
                EndDate = machine.TimeSpan.EndDate
            },
            Reason = machine.Reason,
            Ports = machine.Ports.Select(port => new PortDto { Number = port.Number, Service = port.Service }).ToList(),
            Specification = new SpecificationsDto()
            {
                Memory = machine.Specifications.Memory,
                Storage = machine.Specifications.Storage,
                VirtualProcessors = machine.Specifications.Processors
            },
            Host = new HostDto.Index() { Id = machine.Host.Id, Name = machine.Host.Name },
            Credentials = machine.Credentials.Select(credential => new CredentialsDto
            {
                Username = credential.Username,
                Role = credential.Role,
                PasswordHash = credential.PasswordHash
            }).ToList(),
            Account = new AccountDto.Index()
            {
                Id = machine.Account.Id,
                Email = machine.Account.Email,
                Firstname = machine.Account.Firstname,
                Lastname = machine.Account.Lastname,
                IsActive = machine.Account.IsActive,
                Role = machine.Account.Role
            },
            Requester = new CustomerDto.Index()
            {
                Id = machine.Requester.Id
                ,
                Name = GetFullName(machine.Requester.ContactPerson),
                Email = machine.Requester.ContactPerson.Email,
                CustomerType = GetCustomerType(machine.Requester),
            },
            User = new CustomerDto.Index()
            {
                Id = machine.User.Id,
                Name = GetFullName(machine.User.ContactPerson),
                Email = machine.User.ContactPerson.Email,
                CustomerType = GetCustomerType(machine.User),
            },
            hasVpnConnection = machine.HasVpnConnection
        };
    }

    public async Task<VirtualMachineResponse.GetAllDetails> GetAllDetailsAsync(VirtualMachineRequest.GetAllDetails request)
    {
        VirtualMachineResponse.GetAllDetails response = new();
        var query = virtualMachines.AsQueryable()
            .Include(x => x.User)
            .ThenInclude(x => x.ContactPerson)
            .Include(x => x.Requester)
            .ThenInclude(x => x.ContactPerson)
            .Include(x => x.Account)
            .Include(x => x.Host)
            .Include(x => x.Ports)
            .Include(x => x.Credentials)
            .Include(x => x.Specifications)
            .Include(x => x.TimeSpan)
            .AsNoTracking();

        response.VirtualMachines = await query.Select(machine => ToVirtualMachineDetail(machine)).ToListAsync();
        response.TotalAmount = query.Count();

        return response;
    }

    public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
    {
        VirtualMachineResponse.GetDetail response = new();
        VirtualMachine? machine = await GetMachineById(request.MachineId)
            .Include(x => x.User)
            .ThenInclude(x => x.ContactPerson)
            .Include(x => x.Requester)
            .ThenInclude(x => x.ContactPerson)
            .Include(x => x.Account)
            .Include(x => x.Host)
            .Include(x => x.Ports)
            .Include(x => x.Credentials)
            .Include(x => x.Specifications)
            .Include(x => x.TimeSpan).SingleOrDefaultAsync();

        if (machine is null)
        {
            throw new EntityNotFoundException(nameof(VirtualMachine), request.MachineId);
        }

        response.VirtualMachine = ToVirtualMachineDetail(machine);

        return response;
    }

    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = virtualMachines.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Fqdn.Contains(request.SearchTerm));
        }

        if (request.IsUnfinished)
        {
            query = query.Where(x => x.Status != Status.Deployed);
        }

        response.TotalAmount = query.Count();

        query = query.OrderByDescending(x => x.CreatedAt);
        query = query.Skip((request.Page - 1) * request.Amount);
        query = query.Take(request.Amount);

        response.VirtualMachines = await query.Select(x => new VirtualMachineDto.Index
        {
            Id = x.Id,
            Fqdn = x.Fqdn,
            Status = x.Status

        }).ToListAsync();

        return response;
    }
}
