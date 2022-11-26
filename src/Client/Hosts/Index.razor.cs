using Microsoft.AspNetCore.Components;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Index
{
    [Inject] public IHostService? HostService { get; set; }
    public string? SearchValue { get; set; }

    private IEnumerable<HostDto.Index>? hosts;
    int offset, totalHosts, totalPages = 0;
    int selectedPage = 1;

    protected override async Task OnInitializedAsync()
    {
        hosts = await HostService!.GetIndexAsync(offset);
        totalHosts = await HostService.GetCount();
        totalPages = (totalHosts / 20) + 1;
    }

    async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        hosts = await HostService!.GetIndexAsync(offset);
        selectedPage = pageNr;
    }

    private void ResetFilter()
    {
        SearchValue = "";
    }
}