using Domain.Constants;
using Domain.Hosts;
using Domain.Accounts;
using Domain.VirtualMachines;
using Fakers.VirtualMachines;
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
        VirtualMachineFaker virtualMachineFaker = new();
        machines = virtualMachineFaker.UseSeed(1337).Generate(10);
    }

    // Helper Function
    private CustomerType ReturnsCustomerType(string type)
    {
        CustomerType customerType = CustomerType.None;
        switch (type)
        {
            case "InternalCustomer": customerType = CustomerType.Intern; break;
            case "ExternalCustomer": customerType = CustomerType.Extern; break;
        };
        return customerType;
    }

    public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
    {
        VirtualMachineResponse.Create response = new();

        var x = request.VirtualMachine;

        //var args = new VirtualMachineArgs
        //{
        //    Template = Template.WebServer,
        //    Mode = Mode.IAAS,
        //    Fqdn = "test.com",
        //    Availabilities = new List<Availability> { Availability.Wednesday, Availability.Thursday },
        //    BackupFrequency = BackupFrequency.Daily,
        //    ApplicationDate = DateTime.Now,
        //    TimeSpan = new Domain.VirtualMachines.TimeSpan(startDate: DateTime.Now, endDate: DateTime.Now.AddDays(5)),
        //    Status = Status.Deployed,
        //    Reason = "No Reason",
        //    Ports = new List<Port> { new Port(44, "SSH"), new Port(45, "HTTP") },
        //    Host = null,
        //    Credentials = new List<Credentials> { new Credentials("dummy", "passdummy", "admin") },
        //    Account = new Domain.Accounts.Account("test", "test", "test@mail.com", Role.Admin, "test321", "testdepartment", "testedu"),
        //    Requester = null,
        //    User = null
        //};

        var args = new VirtualMachineArgs
        {
            Template = (Template)Enum.Parse(typeof(Template), x.Template),
            Mode = (Mode)Enum.Parse(typeof(Mode), x.Mode),
            Fqdn = x.Fqdn,
            Availabilities = x.Availabilities,
            BackupFrequency = (BackupFrequency)Enum.Parse(typeof(BackupFrequency), x.BackupFrequency),
            ApplicationDate = x.ApplicationDate,
            TimeSpan = new Domain.VirtualMachines.TimeSpan(startDate: x.StartDate, endDate: x.EndDate),
            Status = (Status)Enum.Parse(typeof(Status), x.Status),
            Reason = x.Reason,
            Ports = x.Ports.Select(x => new Port(x, x.ToString())).ToList(),
            Host = null,
            Credentials = x.Credentials.Select(y => new Credentials(y.Username, y.PasswordHash, y.Role)).ToList(),
            Account = null,
            Requester = null,
            User = null
            //Account = new Domain.Accounts.Account("test", "test", "test@mail.com", Role.Admin, "test321", "testdepartment", "testedu"),
        };

        var machine = new VirtualMachine(args) { Id = machines.Max(x => x.Id) + 1 };
        machines.Add(machine);
        response.MachineId = machine.Id;

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
            Host = (x.Host != null ? new HostDto.Index() { Id = x.Host.Id, Name = x.Host.Name } : null),
            Credentials = x.Credentials.Select(y => new CredentialsDto { Username = y.Username, Role = y.Role, PasswordHash = y.PasswordHash }).ToList(),
            Account = new AccountDto.Index() { Id = x.Account.Id, Email = x.Account.Email, Firstname = x.Account.Firstname, Lastname = x.Account.Lastname, IsActive = x.Account.IsActive, Role = x.Account.Role },
            Requester = new CustomerDto.Index() { Id = x.Requester.Id, Name = (x.Requester.ContactPerson.Firstname + " " + x.Requester.ContactPerson.Lastname), Email = x.Requester.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.Requester.GetType().ToString()) },
            User = new CustomerDto.Index() { Id = x.User.Id, Name = (x.User.ContactPerson.Firstname + " " + x.User.ContactPerson.Lastname), Email = x.User.ContactPerson.Email, CustomerType = ReturnsCustomerType(x.User.GetType().ToString()) },
            hasVpnConnection = x.HasVpnConnection
        }).SingleOrDefault(x => x.Id == request.MachineId) ?? new VirtualMachineDto.Detail();
        return response;
    }

    //public long Id { get; set; }
    //public string Fqdn { get; set; } = default!;
    //public Status Status { get; set; }
    //public string Name { get; set; } = default!;
    //public Template Template { get; set; }
    //public Mode Mode { get; set; }
    //public List<string> Availabilities { get; set; } = default!;
    //public BackupFrequency BackupFrequenty { get; set; }
    //public DateTime ApplicationDate { get; set; } = default!;
    //public TimeSpanDto TimeSpan { get; set; } = default!;
    //public string Reason { get; set; } = default!;
    //public List<PortDto> Ports { get; set; } = default!;
    //public SpecificationsDto Specification { get; set; } = default!;
    //public HostDto.Index Host { get; set; } = default!;
    //public List<CredentialsDto> Credentials { get; set; } = default!;
    //public AccountDto.Index Account { get; set; } = default!;
    //public CustomerDto.Index Requester { get; set; } = default!;
    //public CustomerDto.Index User { get; set; } = default!;
    //public bool hasVpnConnection { get; set; }

    public async Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
    {
        VirtualMachineResponse.GetIndex response = new();
        var query = machines.AsQueryable();

        #region Filteroptions not being used

        //if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        //    query = query.Where(x => x.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));

        //if (!string.IsNullOrWhiteSpace(request.Category))
        //    query = query.Where(x => x.Category.Name.Equals(request.Category, StringComparison.OrdinalIgnoreCase));

        //if (request.MinimumPrice is not null)
        //    query = query.Where(x => x.Price.Value >= request.MinimumPrice);

        //if (request.MaximumPrice is not null)
        //    query = query.Where(x => x.Price.Value <= request.MaximumPrice);

        //if (request.OnlyActiveProducts)
        //    query = query.Where(x => x.IsEnabled);

        #endregion

        response.TotalAmount = query.Count();

        query = query.Skip(request.Amount * request.Page);
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
