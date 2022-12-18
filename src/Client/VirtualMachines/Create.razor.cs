using Client.SharedFiles.Resources;
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
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;
    [Inject] public NavigationManager? Navigation { get; set; }

    private EditForm? Editform { get; set; } = new();
    private VirtualMachineDto.Mutate VirtualMachine { get; set; } = new();
    private PortDto Port { get; set; } = new();

    private List<CredentialsDto> credentialsList = new();
    private CredentialsDto NewCredentials = new();
    //private string selectedAvailability;
    private string _customcss = "background-color: white";


    public List<string> Backups { get; set; } = Enum.GetNames(typeof(BackupFrequency)).ToList();
    public List<string> Modes { get; set; } = Enum.GetNames(typeof(Mode)).ToList();
    public List<string> Templates { get; set; } = Enum.GetNames(typeof(Template)).ToList();
    public List<string> Statuses { get; set; } = Enum.GetNames(typeof(Status)).ToList();
    private List<HostDto.Index>? Hosts { get; set; }
    private List<CustomerDto.Index>? Customers { get; set; }
    private List<AccountDto.Index>? Accounts { get; set; }
    public List<PortDto>? Ports { get; set; } = new();

    public HashSet<PortDto> selectedPorts = new();

    private Dictionary<int, Dictionary<string, string>> _entries = new();

    protected override void OnInitialized()
    {
        _entries.Add(0, new()
        {{"Gebruikernaam", "admin"}, {"Rol", "Admin"}});
        _entries.Add(1, new()
        {{"Gebruikernaam", "admin"}, {"Rol", "User"}});
        for (int i = 0; i < Statuses.Count; i++)
        {
            Statuses[i] = Localizer[Statuses[i]];
        }

        for (int i = 0; i < Backups.Count; i++)
        {
            Backups[i] = Localizer[Backups[i]];
        }

        for (int i = 0; i < Templates.Count; i++)
        {
            Templates[i] = Localizer[Templates[i]];
        }
        //editContext = new(selectedAvailability);
    }

    protected override async Task OnInitializedAsync()
    {
        HostResponse.GetIndex hostResponse = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Amount = 50
        });
        Hosts = hostResponse.Hosts!;

        CustomerResponse.GetIndex customerRequest = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex { Amount = 50 });
        Customers = customerRequest.Customers!;

        AccountResponse.GetIndex accountRequest = await AccountService.GetIndexAsync(new AccountRequest.GetIndex { Amount = 50 });
        Accounts = accountRequest.Accounts!;

        PortResponse.GetAll portRequest = await PortService.GetAllAsync(new PortRequest.GetAll());
        Ports = portRequest.Ports!;
    }

    private bool FetchingResources => false;

    //STATUS
    private Dictionary<string, string> MakeStatusItems()
    {
        return Enum.GetValues(typeof(Status)).Cast<Status>().ToDictionary(x => Localizer[x.ToString()].ToString(), x => x.ToString());
    }

    private void SetStatus(string statusString)
    {
        VirtualMachine.Status = (Status)Enum.Parse(typeof(Status), statusString);
    }

    //MODE
    private Dictionary<string, string> MakeModeItems()
    {
        return Enum.GetValues(typeof(Mode)).Cast<Mode>().ToDictionary(x => x.ToString(), x => x.ToString());
    }

    private void SetMode(string modeString)
    {
        VirtualMachine.Mode = (Mode)Enum.Parse(typeof(Mode), modeString);
    }

    //TEMPLATE
    private Dictionary<string, string> MakeTemplateItems()
    {
        return Enum.GetValues(typeof(Template)).Cast<Template>().ToDictionary(x => Localizer[x.ToString()].ToString(), x => x.ToString());
    }

    private void SetTemplate(string templateString)
    {
        VirtualMachine.Template = (Template)Enum.Parse(typeof(Template), templateString);
    }

    //BACKUPFREQUENCY
    private Dictionary<string, string> MakeBackUpFrequencyItems()
    {
        return Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToDictionary(x => Localizer[x.ToString()].ToString(), x => x.ToString());
    }

    private void SetBackUpFrequency(string backupFrequencyString)
    {
        VirtualMachine.BackupFrequency = (BackupFrequency)Enum.Parse(typeof(BackupFrequency), backupFrequencyString);
    }

    //AVAILABILITIES
    private Dictionary<string, string> MakeAvailibilityItems()
    {
        return Enum.GetValues(typeof(Availability)).Cast<Availability>().ToDictionary(x => Localizer![x.ToString()].ToString(), x => x.ToString());
    }

    private HashSet<Availability> chosenDays = new();
    private void AddDay(string value)
    {
        chosenDays.Add((Availability)Enum.Parse(typeof(Availability), value, true));
    }
    private void RemoveDay(Availability value)
    {
        chosenDays.Remove(value);
    }

    //PORT
    private Dictionary<string, string> MakePortItems()
    {
        return Ports.ToDictionary(x => x.Service.ToString(), x => JsonConvert.SerializeObject(x));
    }

    private void SetPortValue(string value)
    {
        var port = JsonConvert.DeserializeObject<PortDto>(value)!;
        if (!selectedPorts.Where(x => x.Service == port.Service).Any())
        {
            selectedPorts.Add(port);
        }

    }
    public void UnSelectPort(PortDto port)
    {
        selectedPorts.Remove(port);
    }

    //HOST
    private Dictionary<string, string> MakeHostItems()
    {
        return Hosts.ToDictionary(x => x.Name.ToString(), x => JsonConvert.SerializeObject(x));
    }

    private void SetHostValue(string value)
    {
        VirtualMachine.HostId = JsonConvert.DeserializeObject<HostDto.Index>(value)!.Id;
    }

    //TODO - REQUESTER & USER (Key has dupplicates?)
    private Dictionary<string, string> MakeCustomerItems()
    {
        return Customers.ToDictionary(x => x.Id + " " + x.Name, x => JsonConvert.SerializeObject(x));
    }

    private void SetRequester(string requesterIdString)
    {
        VirtualMachine.RequesterId = JsonConvert.DeserializeObject<CustomerDto.Index>(requesterIdString)!.Id;
    }
    private void SetUser(string userIdString)
    {
        VirtualMachine.UserId = JsonConvert.DeserializeObject<CustomerDto.Index>(userIdString)!.Id;
    }

    //ACCOUNT
    private Dictionary<string, string> MakeAccountItems()
    {
        return Accounts.ToDictionary(x => string.Format("{0} {1}", x.Firstname, x.Lastname).ToString(), x => JsonConvert.SerializeObject(x));
    }

    private void SetAccount(string accountIdString)
    {
        VirtualMachine.AdministratorId = JsonConvert.DeserializeObject<AccountDto.Index>(accountIdString)!.Id;
    }


    private void ClickHandler(int id)
    {
        _entries.Remove(id);
    }


    private void AddCredential()
    {
        credentialsList.Add(NewCredentials);
        NewCredentials = new();
    }

    private void RemoveCredentials(CredentialsDto credentials)
    {
        credentialsList.Remove(credentials);
    }

    private async void HandleValidSubmit()
    {

        VirtualMachine.Credentials = credentialsList;
        VirtualMachine.Ports = selectedPorts.Select(x => x.Number).ToList();
        VirtualMachine.Availabilities = chosenDays.ToList();

        VirtualMachineResponse.Create response = await VirtualMachineService.CreateAsync(new VirtualMachineRequest.Create
        {
            VirtualMachine = VirtualMachine
        });
        Navigation!.NavigateTo("virtual-machine/" + response.MachineId);
    }
}