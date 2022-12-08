using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;


namespace Client.VirtualMachines;

public partial class Details
{
    [Inject] private IVirtualMachineService? VirtualMachineService { get; set; }
    [Inject] private NavigationManager? Navigation { get; set; }
    [Inject] private IStringLocalizer<Shared.Resources.Resource>? localizer { get; set; }
    [Parameter] public long Id { get; set; }

    //Model
    private VirtualMachineDto.Detail? virtualMachine;

    private Dictionary<string, string> _defaultInformation = new();
    private Dictionary<string, string> _specs = new();
    private Dictionary<string, string> _config = new();
    private Dictionary<string, string> _ports = new();
    private Dictionary<string, string> _available = new();
    private Dictionary<string, string> _backups = new();
    private Dictionary<string, string> _user = new();
    private Dictionary<string, string> _requester = new();
    private Dictionary<string, string> _personincharge = new();
    private List<Dictionary<string, string>> _logindata = new();
    private Dictionary<string, string> _host = new();
    //private Dictionary<string, string> _logindata = new();


    protected async override Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetDetail response = await VirtualMachineService.GetDetailAsync(new VirtualMachineRequest.GetDetail() { MachineId = Id }) ?? new VirtualMachineResponse.GetDetail();
        virtualMachine = response.VirtualMachine;


        _defaultInformation.Add("Naam", virtualMachine!.Name);
        _defaultInformation.Add("FQDN", virtualMachine!.Fqdn);
        _defaultInformation.Add("Hostnaam", virtualMachine!.Host.Name);
        _config.Add("Mode", virtualMachine.Mode.ToString());
        _config.Add("Template", localizer![virtualMachine.Template.ToString()]);
        _config.Add("Reden", virtualMachine.Reason);
        _specs.Add("vCPUs", virtualMachine.Specification.VirtualProcessors.ToString());
        _specs.Add("Geheugen", string.Format("{0} GB", virtualMachine.Specification.Memory.ToString()));
        _specs.Add("Opslag", string.Format("{0} GB", virtualMachine.Specification.Storage.ToString()));
        //if (virtualMachine.Availabilities.Count>1){
        //    _available.Add("Beschikbaarheid", string.Join(", ", virtualMachine.Availabilities));
        //}
        _available.Add("Backups", localizer[virtualMachine.BackupFrequenty.ToString()]);
        _backups.Add("Aangevraagd op", virtualMachine.ApplicationDate.ToShortDateString());
        _backups.Add("Start", virtualMachine.TimeSpan.StartDate.ToShortDateString());
        _backups.Add("Einde", virtualMachine.TimeSpan.EndDate.ToShortDateString());
        _personincharge.Add("Opgezet door", virtualMachine.Account?.Firstname + " " + virtualMachine.Account?.Lastname);
        _personincharge.Add("Ondersteuning", virtualMachine.Account?.Firstname + " " + virtualMachine.Account?.Lastname);
        _user.Add("Naam", virtualMachine!.User.Name);
        _user.Add("Email", virtualMachine!.User.Email);
        _requester.Add("Naam", virtualMachine!.Requester.Name);
        _requester.Add("Email", virtualMachine!.Requester.Email);
        _ports.Add("Externe Toegang", string.Join(", ", virtualMachine.Ports.Select(p => p.Service)));
        _ports.Add("VPN", virtualMachine.hasVpnConnection.ToString());
        foreach (var credential in virtualMachine.Credentials) _logindata.Add(new Dictionary<string, string>() { { "Gebruikersnaam", credential.Username }, { "Rol", credential.Role } });
        _host.Add("Naam", virtualMachine.Host.Name);
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("virtual-machine/list");
    }
}