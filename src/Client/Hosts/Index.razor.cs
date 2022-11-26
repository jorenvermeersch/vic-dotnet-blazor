using Microsoft.AspNetCore.Components;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Index
{
    [Inject] public IHostService HostService { get; set; } = default!;
    public string? SearchValue { get; set; }

    private IEnumerable<HostDto.Index>? hosts;

    private int offset, totalHosts, totalPages = 0;
    private int selectedPage = 1;

    protected override async Task OnInitializedAsync()
    {
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Offset = offset,
        });
        hosts = response.Hosts;
        totalHosts = response.TotalAmount;
        totalPages = (totalHosts / 20) + 1;
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Offset = offset,
        });
        hosts = response.Hosts;
        selectedPage = pageNr;
    }

    private void ResetFilter()
    {
        SearchValue = "";
    }
}