using Client.Extensions;
using Microsoft.AspNetCore.Components;
using Shared.Hosts;
using Shared.VirtualMachines;

namespace Client.Hosts;

public partial class Details
{
    private HostDto.Detail? host;

    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;

    [Parameter]
    public long Id { get; set; }

    private Dictionary<string, Dictionary<string, string>> datacards = new();
    private string NAME_KEY = "NAME";
    private string SPECIFICATIONS_KEY = "SPECIFICATIONS";
    private string REMAINING_SPECIFICATIONS_KEY = "REMAINING_SPECIFICATIONS";

    private List<Dictionary<string, string>> processors = new();

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

        datacards = new()
        {
            {
                NAME_KEY,
                new()
                {
                    {  "Naam", host.Name },
                }
            },
            {
                SPECIFICATIONS_KEY,
                new()
                {
                    { "vCPUs", CalculateTotalVirtualProcessors().ToString() },
                    { "Geheugen", host.Specifications.Memory.ToString().GbFormat() },
                    { "Opslag", host.Specifications.Storage.ToString().GbFormat() }

                }
            },
            {
                REMAINING_SPECIFICATIONS_KEY,
                new()
                {
                    {  "vCPUs", host.RemainingResources.VirtualProcessors.ToString() },
                    { "Geheugen", host.RemainingResources.Memory.ToString().GbFormat() },
                    { "Opslag", host.RemainingResources.Storage.ToString().GbFormat() }

                }
            },
        };

        foreach (var processor in host.Specifications.Processors)
        {
            processors.Add(
                new()
                {
                    { "Type", processor.Key.Name },
                    { "Cores", processor.Key.Cores.ToString() },
                    { "Threads", processor.Key.Threads.ToString() },
                    { "Virtualisatiefactor", processor.Value.ToString() }
                }
            );
        }


        totalVirtualMachines = host?.Machines?.Count() ?? 0;
        totalPages = (totalVirtualMachines / 10) + 1;
    }

    private void NavigateBack()
    {
        Navigation.NavigateTo("host/list");
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        selectedPage = pageNr;
    }
    private int CalculateTotalVirtualProcessors()
    {
        return host!.Specifications.Processors.Select(pair => pair.Key.Cores * pair.Value).Sum();
    }
}