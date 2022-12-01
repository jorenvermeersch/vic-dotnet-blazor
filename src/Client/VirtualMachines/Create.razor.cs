using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

namespace Client.VirtualMachines;

public partial class Create
{
    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;
    [Inject] public IAccountService AccountService { get; set; } = default!;
    [Inject] public IPortService PortService { get; set; } = default!;
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public IStringLocalizer<Shared.Resources.Resource>? localizer { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }


    private EditForm? Editform { get; set; } = new();
    private VirtualMachineDto.Mutate VirtualMachine { get; set; } = new();
    private PortDto Port { get; set; } = new();

    private SpecificationsDto? Specifications;
    private string SelectedPort { get; set; } = "";
    // Credentials
    private List<CredentialsDto> credentialList = new();
    private CredentialsDto? newCredential = new();
    private List<string> Availabilities = new();
    private string selectedAvailability;
    private string _customcss = "background-color: white";
    private HashSet<string> _ports = new();
    public List<string> PortOptions { get; set; }

    public List<string> Backups { get; set; } = Enum.GetNames(typeof(BackupFrequency)).ToList();
    public List<string> Modes { get; set; } = Enum.GetNames(typeof(Mode)).ToList();
    public List<string> Templates { get; set; } = Enum.GetNames(typeof(Template)).ToList();
    public List<string> Statuses { get; set; } = Enum.GetNames(typeof(Status)).ToList();
    //private List<string>? Hosts { get; set; }
    private List<HostDto.Index>? Hosts { get; set; }

    private List<string>? Customers { get; set; }

    private List<string>? Accounts { get; set; } = new();
    private Dictionary<int, Dictionary<string, string>> _entries = new();
    private IEnumerable<PortDto> availablePorts;
    private string Requester { get; set; }

    private string User { get; set; }

    private string Host { get; set; }

    private string Account { get; set; }

    private Dictionary<string, string> hosts = null;

    protected override void OnInitialized()
    {
        Specifications = VirtualMachine.Specifications;
        _entries.Add(0, new()
        {{"Gebruikernaam", "admin"}, {"Rol", "Admin"}});
        _entries.Add(1, new()
        {{"Gebruikernaam", "admin"}, {"Rol", "User"}});
        for (int i = 0; i < Statuses.Count; i++)
        {
            Statuses[i] = localizer[Statuses[i]];
        }

        for (int i = 0; i < Backups.Count; i++)
        {
            Backups[i] = localizer[Backups[i]];
        }

        for (int i = 0; i < Templates.Count; i++)
        {
            Templates[i] = localizer[Templates[i]];
        }
        //editContext = new(selectedAvailability);
    }

    protected override async Task OnInitializedAsync()
    {
        HostResponse.GetIndex hostResponse = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Offset = 0
        });
        Hosts = hostResponse.Hosts;

        //Hosts.ForEach(x => Console.WriteLine(x.Name.ToString()));
        //hosts = Hosts.ToDictionary(x => x.Name, x => JsonConvert.SerializeObject(x));

        //Console.WriteLine(hosts);

        //IEnumerable<HostDto.Index> hostIndexes = hostResponse.Hosts ?? new List<HostDto.Index>();
        //Hosts = hostIndexes.Select(h => string.Concat(h.Id, " - ", h.Name)).ToList();



        CustomerResponse.GetIndex customerResponse = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Offset = 0
        });
        IEnumerable<CustomerDto.Index> customerIndexes = customerResponse.Customers ?? new List<CustomerDto.Index>();
        Customers = customerIndexes.Select(c => string.Concat(c.Id, " - ", c.Name)).ToList();


        AccountResponse.GetIndex accountResponse = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Offset = 0
        });
        IEnumerable<AccountDto.Index> accountIndexes = accountResponse.Accounts ?? new List<AccountDto.Index>();
        Accounts = accountIndexes.Select(c => string.Concat(c.Id, " - ", c.Firstname, " ", c.Lastname, " - ", c.Role)).ToList();

        //PortResponse.GetAll portsResponse = await PortService.GetAllAsync(new PortRequest.GetAll());
        //availablePorts = portsResponse.Ports ?? new List<PortDto>();
        //PortOptions = availablePorts.Select(p => string.Concat(p.Number + " - " + p.Service)).ToList();
    }


    private Dictionary<string, string> MakeStatusItems() => Enum.GetValues(typeof(Status)).Cast<Status>().ToDictionary(x => x.ToString(), x => x.ToString());
    private void SetStatusValue(string value) => VirtualMachine.Status = (Status)Enum.Parse(typeof(Status), value);

    private Dictionary<string, string> MakeModeItems() => Enum.GetValues(typeof(Mode)).Cast<Mode>().ToDictionary(x => x.ToString(), x => x.ToString());
    private void SetModeValue(string value) => VirtualMachine.Mode = (Mode)Enum.Parse(typeof(Mode), value);

    private Dictionary<string, string> MakeTemplateItems() => Enum.GetValues(typeof(Template)).Cast<Template>().ToDictionary(x => x.ToString(), x => x.ToString());
    private void SetTemplateValue(string value) => VirtualMachine.Template = (Template)Enum.Parse(typeof(Template), value);
    private Dictionary<string, string> MakeBackUpFrequencyItems() => Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToDictionary(x => x.ToString(), x => x.ToString());
    private void SetBackUpFrequencyValue(string value) => VirtualMachine.BackupFrequency = (BackupFrequency)Enum.Parse(typeof(BackupFrequency), value);
    private Dictionary<string, string> MakeHostItems() => Hosts.ToDictionary(x => x.Name.ToString(), x => JsonConvert.SerializeObject(x));
    private void SetHostValue(string value)
    {
        VirtualMachine.Host = JsonConvert.DeserializeObject<HostDto.Index>(value);
    }



    private void ClickHandler(int id)
    {
        _entries.Remove(id);
    }

    public void AddPortToList()
    {
        if (SelectedPort != "") _ports.Add(SelectedPort);
    }

    public void RemovePortFromList(string value)
    {
        _ports.Remove(value);
    }

    private void AddAvailability()
    {
        Availabilities.Add(selectedAvailability);
    }

    private void AddCredential()
    {
        credentialList.Add(newCredential!);
        newCredential = new();
    }

    private async void HandleValidSubmit()
    {
        List<PortDto> chosenPorts = new();
        VirtualMachine.Credentials = credentialList;
        foreach (var port in _ports)
        {
            chosenPorts.Add(availablePorts.Where(p => p.Number.ToString() == port.Split(" ")[0]).First());
        }

        VirtualMachine.Ports = chosenPorts.Select(p => p.Number).ToList();


        VirtualMachineResponse.Create response = await VirtualMachineService.CreateAsync(new VirtualMachineRequest.Create
        {
            VirtualMachine = VirtualMachine
        });
        Navigation!.NavigateTo("virtual-machine/" + response.MachineId);
    }
}