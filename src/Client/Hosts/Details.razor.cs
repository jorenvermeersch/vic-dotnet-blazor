using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Shared.Hosts;
using Shared.VirtualMachines;


namespace Client.Hosts;

public partial class Details
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;


    [Parameter]
    public long Id { get; set; }

    //Model
    private HostDto.Detail? host;

    private Dictionary<string, Dictionary<string, string>> _server = new();
    private List<Dictionary<string, string>> _processors = new();
    private IEnumerable<VirtualMachineDto.Index>? virtualMachines;
    private int offset, totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;

    protected override async Task OnInitializedAsync()
    {
        HostResponse.GetDetail response = await HostService.GetDetailAsync(new HostRequest.GetDetail
        {
            HostId = Id
        });

        host = response.Host;
        _server.Add("name", new() { { "Naam", host.Name } });
        _server.Add("resources", new() {{ "Geheugen", host.Specifications.Memory.ToString() }, { "Opslag", host.Specifications.Storage.ToString() } });
        _server.Add("remainingResources", new() { { "vCPU", host.RemainingResources.VirtualProcessors.ToString() }, { "Geheugen", host.RemainingResources.Memory.ToString() }, { "Opslag", host.RemainingResources.Storage.ToString() } });
        foreach (var p in host.Specifications.Processors)
        {
            _processors.Add(new Dictionary<string, string>() { {"Processortype", p.Key.Name }, { "Aantal Threads", p.Key.Threads.ToString() }, { "Aantal Cores", p.Key.Cores.ToString() }, { "Virtualisatiefactor", p.Value.ToString()} });
        }
        totalVirtualMachines = host?.Machines?.Count() ?? 0;
        totalPages = (totalVirtualMachines / 10) + 1;
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("host/list");
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        selectedPage = pageNr;
    }
}