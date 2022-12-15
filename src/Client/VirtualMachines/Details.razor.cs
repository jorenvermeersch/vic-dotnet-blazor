using Client.Extensions;
using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;


namespace Client.VirtualMachines;

public partial class Details
{
    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;
    [Parameter] public long Id { get; set; }

    private VirtualMachineDto.Detail? machine;

    #region Dictionaries
    private Dictionary<string, string> generalInformation = new();
    private Dictionary<string, string> specifications = new();
    private Dictionary<string, string> configuration = new();
    private Dictionary<string, string> ports = new();
    private Dictionary<string, string> availabilities = new();
    private Dictionary<string, string> backup = new();
    private Dictionary<string, string> user = new();
    private Dictionary<string, string> requester = new();
    private Dictionary<string, string> administrator = new();
    private List<Dictionary<string, string>> loginCredentials = new();
    private Dictionary<string, string> host = new();
    #endregion


    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetDetail response = await VirtualMachineService.GetDetailAsync(
            new VirtualMachineRequest.GetDetail()
            {
                MachineId = Id
            }
        );
        machine = response.VirtualMachine;

        SetGeneralInformation();

        host.Add("Hostnaam", machine!.Host.Name);
        configuration.Add("Mode", machine.Mode.ToString());
        configuration.Add("Template", Localizer![machine.Template.ToString()]);
        configuration.Add("Reden", machine.Reason);
        specifications.Add("vCPUs", machine.Specification.VirtualProcessors.ToString());
        specifications.Add("Geheugen", machine.Specification.Memory.ToString().GbFormat());
        specifications.Add("Opslag", machine.Specification.Storage.ToString().GbFormat());
        availabilities.Add("Backups", Localizer[machine.BackupFrequenty.ToString()]);
        backup.Add("Aangevraagd op", machine.ApplicationDate.FormatDate());
        backup.Add("Start", machine.TimeSpan.StartDate.FormatDate());
        backup.Add("Einde", machine.TimeSpan.EndDate.FormatDate());
        administrator.Add("Opgezet door", machine.Account.GetFullName());
        user.Add("Gebruiker", machine!.User.Name);
        requester.Add("Aanvrager", machine!.Requester.Name);
        ports.Add("Externe Toegang", string.Join(", ", machine.Ports.Select(p => p.Service)));
        ports.Add("VPN", machine.hasVpnConnection.ToString());
        foreach (var credential in machine.Credentials) loginCredentials.Add(new Dictionary<string, string>() { { "Gebruikersnaam", credential.Username }, { "Rol", credential.Role } });
    }

    #region Dictionary setters
    private void SetGeneralInformation()
    {
        generalInformation = new()
        {
            { "Naam", machine!.Name },
            { "FQDN", machine!.Fqdn }
        };
    }

    //TODO: Add this setter for all dictionaries. 
    #endregion


    #region Navigate functions
    private void NavigateBack()
    {
        Navigation.NavigateTo("virtual-machine/list");
    }

    private void NavigateToRequester()
    {

        Navigation.NavigateTo($"customer/{machine!.Requester.Id}");
    }

    private void NavigateToUser()
    {
        Navigation.NavigateTo($"customer/{machine!.User.Id}");
    }

    private void NavigateToHost()
    {
        Navigation.NavigateTo($"host/{machine!.Host.Id}");
    }

    private void NavigateToAdministrator()
    {
        Navigation.NavigateTo($"host/{machine!.Account.Id}");
    }
    #endregion
}