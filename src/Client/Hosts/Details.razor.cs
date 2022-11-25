using Microsoft.AspNetCore.Components;
using Shared.Host;
using Shared.VirtualMachine;


namespace Client.Hosts;

public partial class Details
{
    [Inject] public NavigationManager? Navigation { get; set; }
    [Inject] public IHostService? HostService { get; set; }


    [Parameter]
    public long Id { get; set; }

    //Model
    private HostDto.Detail? host;

    private Dictionary<string, Dictionary<string, string>> _server = new();
    private IEnumerable<VirtualMachineDto.Index> virtualMachines;
    private int offset, totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;

    protected override async Task OnInitializedAsync()
    {
        host = await HostService!.GetDetailAsync(Id);
        _server.Add("name", new() { { "Naam", host.Name } });
        _server.Add("resources", new() { { "vCPU", host.Specifications.Processors.ToString() }, { "Geheugen", host.Specifications.Memory.ToString() }, { "Opslag", host.Specifications.Storage.ToString() } });
        _server.Add("remainingResources", new() { { "vCPU", host.RemainingResources.Processors.ToString() }, { "Geheugen", host.RemainingResources.Memory.ToString() }, { "Opslag", host.RemainingResources.Storage.ToString() } });
        //virtualMachines = await virtualMachineService.GetVirtualMachinesByHostId(host.Id,offset);
        totalVirtualMachines = virtualMachines.Count();
        totalPages = (totalVirtualMachines / 10) + 1;
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("host/list");
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        //virtualMachines = await virtualMachineService.GetVirtualMachinesByHostId(host.Id, offset);
        selectedPage = pageNr;
    }
}