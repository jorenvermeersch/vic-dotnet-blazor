using Microsoft.AspNetCore.Components;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Index
{
    [Inject] public IHostService HostService { get; set; } = default!;
    public string? SearchValue { get; set; }

    private IEnumerable<HostDto.Index>? hosts;

    private int totalHosts, totalPages = 0;
    private readonly int amount = 20;
    private int selectedPage = 1;

    protected override async Task OnInitializedAsync()
    {
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page = 1,
            Amount = amount,
        });
        hosts = response.Hosts;
        totalHosts = response.TotalAmount;
        totalPages = totalHosts / amount + (totalHosts % amount > 0 ? 1 : 0);
    }

    private async Task ClickHandler(int pageNr)
    {
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm = SearchValue
        });
        hosts = response.Hosts;
        selectedPage = pageNr;
    }

    private async void ResetFilter()
    {
        SearchValue = "";
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page = 1,
            Amount = amount,
            SearchTerm = SearchValue
        });
        hosts = response.Hosts;
        totalHosts = response.TotalAmount;
        totalPages = totalHosts / amount + (totalHosts % amount > 0 ? 1 : 0);
        selectedPage = 1;
        StateHasChanged();
    }

    private async void HandleFilter()
    {
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page=1,
            Amount = amount,
            SearchTerm = SearchValue
        });
        hosts = response.Hosts;
        totalHosts = response.TotalAmount;
        totalPages = totalHosts / amount + (totalHosts % amount > 0 ? 1 : 0);
        StateHasChanged();
    }
}