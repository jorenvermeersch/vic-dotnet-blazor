using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Shared.Account;
using Shared.Customer;
using Shared.Host;
using Shared.Port;
using Shared.Specification;
using Shared.VirtualMachine;

namespace Client.VirtualMachines;

public partial class Create
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; }
    [Inject] public IHostService? HostService { get; set; }
    [Inject] public IAccountService? AccountService { get; set; }
    [Inject] public IPortService? PortService { get; set; }
    [Inject] public ICustomerService? CustomerService { get; set; }
    [Inject] public IStringLocalizer<Shared.Resources.Resource>? localizer { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }


    private EditForm? Editform { get; set; } = new();
    private VirtualMachineDto.Create VirtualMachine { get; set; } = new();
    private PortDto Port { get; set; } = new();
    private SpecificationDto Specifications;
    private string SelectedPort { get; set; } = "";
    // Credentials
    private List<CredentialDto> credentialList = new();
    private CredentialDto? newCredential = new();
    private List<string> Availabilities = new();
    private string selectedAvailability;
    private string _customcss = "background-color: white";
    private HashSet<string> _ports = new();
    public List<string> PortOptions { get; set; }

    public List<string> Backups { get; set; } = Enum.GetNames(typeof(BackupFrequency)).ToList();
    public List<string> Modes { get; set; } = Enum.GetNames(typeof(Mode)).ToList();
    public List<string> Templates { get; set; } = Enum.GetNames(typeof(Template)).ToList();
    public List<string> Statuses { get; set; } = Enum.GetNames(typeof(Status)).ToList();
    private List<string>? Hosts { get; set; }

    private List<string>? Customers { get; set; }

    private List<string>? Accounts { get; set; } = new();
    private Dictionary<int, Dictionary<string, string>> _entries = new();
    private IEnumerable<PortDto> availablePorts;
    private string Requester { get; set; }

    private string User { get; set; }

    private string Host { get; set; }

    private string Account { get; set; }

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
        IEnumerable<HostDto.Index> hosts = await HostService!.GetIndexAsync(0);
        Hosts = hosts.Select(h => string.Concat(h.Id, " - ", h.Name)).ToList();
        IEnumerable<CustomerDto.Index> customers = await CustomerService!.GetIndexAsync(0);
        Customers = customers.Select(c => string.Concat(c.Id, " - ", c.Name)).ToList();
        AccountResult.Index accountResponse = await AccountService!.GetIndexAsync(new AccountRequest.Index());
        IEnumerable<AccountDto.Index> accounts = accountResponse.Accounts;
        Accounts = accounts.Select(c => string.Concat(c.Id, " - ", c.Firstname, " ", c.Lastname, " - ", c.Role)).ToList();
        availablePorts = await PortService!.GetIndexAsync();
        PortOptions = availablePorts.Select(p => string.Concat(p.Number + " - " + p.Service)).ToList();
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

        VirtualMachine.Ports = chosenPorts;
        VirtualMachineDto.Details newVirtualMachine = await VirtualMachineService!.Add(VirtualMachine);
        Navigation!.NavigateTo("virtual-machine/" + newVirtualMachine.Id);
    }
}