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
    [Inject] public IStringLocalizer<Shared.Resources.Resource> localizer { get; set; } = default!;
    [Inject] public NavigationManager? Navigation { get; set; }

    private EditForm? Editform { get; set; } = new();
    private VirtualMachineDto.Mutate VirtualMachine { get; set; } = new();
    private PortDto Port { get; set; } = new();

    private SpecificationsDto? Specifications;
    // Credentials
    private List<CredentialsDto> credentialList = new();
    private CredentialsDto? newCredential = new();
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
    public HashSet<PortDto>? selectedPorts = new();

    private Dictionary<int, Dictionary<string, string>> _entries = new();

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

    //STATUS
    private Dictionary<string, string> MakeStatusItems() => Enum.GetValues(typeof(Status)).Cast<Status>().ToDictionary(x => localizer[x.ToString()].ToString(), x => x.ToString());
    private void SetStatusValue(string value) => VirtualMachine.Status = (Status)Enum.Parse(typeof(Status), value);

    //MODE
    private Dictionary<string, string> MakeModeItems() => Enum.GetValues(typeof(Mode)).Cast<Mode>().ToDictionary(x => x.ToString(), x => x.ToString());
    private void SetModeValue(string value) => VirtualMachine.Mode = (Mode)Enum.Parse(typeof(Mode), value);

    //TEMPLATE
    private Dictionary<string, string> MakeTemplateItems() => Enum.GetValues(typeof(Template)).Cast<Template>().ToDictionary(x => localizer[x.ToString()].ToString(), x => x.ToString());
    private void SetTemplateValue(string value) => VirtualMachine.Template = (Template)Enum.Parse(typeof(Template), value);

    //BACKUPFREQUENCY
    private Dictionary<string, string> MakeBackUpFrequencyItems() => Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToDictionary(x => localizer[x.ToString()].ToString(), x => x.ToString());
    private void SetBackUpFrequencyValue(string value) => VirtualMachine.BackupFrequency = (BackupFrequency)Enum.Parse(typeof(BackupFrequency), value);

    //AVAILABILITIES
    private Dictionary<string, string> MakeAvailibilityItems() => Enum.GetValues(typeof(Availability)).Cast<Availability>().ToDictionary(x => localizer![x.ToString()].ToString(), x => x.ToString());
    private HashSet<Availability> chosenAvailabilities = new();
    private void SetAvailabilityValue(string value)
    {
        chosenAvailabilities.Add((Availability)Enum.Parse(typeof(Availability), value, true));
    }
    private void RemoveAvailabilityFromList(Availability value)
    {
        chosenAvailabilities.Remove(value);
    }

    //PORT
    private Dictionary<string, string> MakePortItems() => Ports.ToDictionary(x => x.Service.ToString(), x => JsonConvert.SerializeObject(x));
    private void SetPortValue(string value)
    {
        var port = JsonConvert.DeserializeObject<PortDto>(value)!;
        if (!selectedPorts.Where(x => x.Service == port.Service).Any())
        {
            selectedPorts.Add(port);
        }

    }
    public void RemovePortFromList(PortDto port)
    {
        selectedPorts.Remove(port);
    }

    //HOST
    private Dictionary<string, string> MakeHostItems() => Hosts.ToDictionary(x => x.Name.ToString(), x => JsonConvert.SerializeObject(x));
    private void SetHostValue(string value)
    {
        VirtualMachine.HostId = JsonConvert.DeserializeObject<HostDto.Index>(value)!.Id;
    }

    //TODO - REQUESTER & USER (Key has dupplicates?)
    private Dictionary<string, string> MakeCustomerItems() => Customers.ToDictionary(x => x.Id + " " + x.Name, x => JsonConvert.SerializeObject(x));
    private void SetRequesterValue(string value)
    {
        VirtualMachine.RequesterId = JsonConvert.DeserializeObject<CustomerDto.Index>(value)!.Id;
    }
    private void SetUserValue(string value)
    {
        VirtualMachine.UserId = JsonConvert.DeserializeObject<CustomerDto.Index>(value)!.Id;
    }

    //ACCOUNT
    private Dictionary<string, string> MakeAccountItems() => Accounts.ToDictionary(x => string.Format("{0} {1}", x.Firstname, x.Lastname).ToString(), x => JsonConvert.SerializeObject(x));
    private void SetAccountValue(string value)
    {
        VirtualMachine.AdministratorId = JsonConvert.DeserializeObject<AccountDto.Index>(value)!.Id;
    }


    private void ClickHandler(int id)
    {
        _entries.Remove(id);
    }


    private void AddCredential()
    {
        credentialList.Add(newCredential!);
        newCredential = new();
    }

    private async void HandleValidSubmit()
    {

        VirtualMachine.Credentials = credentialList;
        VirtualMachine.Ports = selectedPorts.Select(x => x.Number).ToList();
        VirtualMachine.Availabilities = chosenAvailabilities.ToList();

        VirtualMachineResponse.Create response = await VirtualMachineService.CreateAsync(new VirtualMachineRequest.Create
        {
            VirtualMachine = VirtualMachine
        });
        Navigation!.NavigateTo("virtual-machine/" + response.MachineId);
    }
}