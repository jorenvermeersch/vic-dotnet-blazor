using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;

namespace Client.VirtualMachines;

public partial class Table
{
    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string searchString = "";

    private string style = "white-space: nowrap";

    private VirtualMachineDto.Detail? chosenMachine = null;
    private IEnumerable<VirtualMachineDto.Detail>? virtualMachines = null;

    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;


    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetAllDetails response = await VirtualMachineService.GetAllDetailsAsync(new VirtualMachineRequest.GetAllDetails()) ?? new VirtualMachineResponse.GetAllDetails();
        virtualMachines = response.VirtualMachines;
    }

    private bool FilterTable(VirtualMachineDto.Detail element)
    {
        return FilterTable(element, searchString);
    }

    private bool FilterTable(VirtualMachineDto.Detail element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Fqdn.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Template.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Mode.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.BackupFrequenty.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Reason.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (string.Join(", ", element.Ports.Select(x => x.Service)).Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Host.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if ((element.Account.Firstname + element.Account.Lastname).Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Requester.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.User.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

}
