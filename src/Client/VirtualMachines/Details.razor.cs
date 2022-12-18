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

    private Dictionary<string, Dictionary<string, string>> datacards = new();
    private string GENERAL_INFORMATION_KEY = "GENERAL_INFORMATION";
    private string HOST_NAME_KEY = "HOST_NAME";
    private string SPECIFICATIONS_KEY = "SPECIFICATIONS";
    private string CONFIGURATION_KEY = "CONFIGURATION";
    private string PORTS_KEY = "PORTS";
    private string BACKUPS_KEY = "BACKUPS";
    private string AVAILABILITY_KEY = "AVAILABILITY";
    private string REQUESTER_KEY = "REQUESTER";
    private string USER_KEY = "USER";
    private string ADMIN_KEY = "ADMIN";

    private List<Dictionary<string, string>> credentials = new();

    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetDetail response = await VirtualMachineService.GetDetailAsync(
            new VirtualMachineRequest.GetDetail()
            {
                MachineId = Id
            }
        );
        machine = response.VirtualMachine;

        datacards = new()
        {
            {
                GENERAL_INFORMATION_KEY,
                new()
                {
                    { "Naam", machine.Name },
                    { "FQDN", machine.Fqdn }
                }
            },
            {
                HOST_NAME_KEY,
                new()
                {
                    { "Hostnaam", machine.Host.Name }
                }
            },
            {
                SPECIFICATIONS_KEY,
                new()
                {
                    { "vCPUs", machine.Specification.VirtualProcessors.ToString() },
                    { "Geheugen", machine.Specification.Memory.ToString().GbFormat() },
                    { "Opslag", machine.Specification.Storage.ToString().GbFormat() }
                }
            },
            {
                CONFIGURATION_KEY,
                new()
                {
                    { "Mode", machine.Mode.ToString() },
                    { "Template", Localizer![machine.Template.ToString()] },
                    { "Reden", machine.Reason }
                }
            },
            {
                PORTS_KEY,
                new()
                {
                    { "Externe toegang", string.Join(", ", machine.Ports.Select(port => port.Service)) },
                    { "VPN", machine.hasVpnConnection ? "Ja" : "Neen" },
                }
            },
            {
                BACKUPS_KEY,
                new()
                {
                    { "Back-ups", Localizer[machine.BackupFrequenty.ToString()] },
                }
            },
            {
                AVAILABILITY_KEY,
                new()
                {
                    { "Aangevraagd op", machine.ApplicationDate.FormatDate() },
                    { "Start", machine.TimeSpan.StartDate.FormatDate() },
                    { "Einde", machine.TimeSpan.EndDate.FormatDate() },
                }
            },
            {
                REQUESTER_KEY,
                new()
                {
                    { "Aanvrager", machine.Requester.Name },

                }
            },
            {
                USER_KEY,
                new()
                {
                    { "Gebruiker", machine.User.Name },

                }
            },
            {
                ADMIN_KEY,
                new()
                {
                    { "Opgezet door", machine.Account.GetFullName() },

                }
            },

        };

        // TODO: Ports do not get fetched properly. GetDetail. 
        // TODO: VirtualProcessors always have value 0 after creating manually. 

        Console.WriteLine($"processsors: {machine.Specification.VirtualProcessors}");

        foreach (var credential in machine.Credentials)
        {
            credentials.Add(new() { { "Gebruikersnaam", credential.Username }, { "Rol", credential.Role } });
        }


    }

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