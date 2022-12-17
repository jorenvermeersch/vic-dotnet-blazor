using Microsoft.AspNetCore.Components;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Index
{
    [Inject] public IHostService HostService { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? SearchValue { get; set; }
    [Parameter, SupplyParameterFromQuery] public int Page { get; set; } = 1;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<HostDto.Index>? hosts;

    private int totalHosts, totalPages = 0;
    private readonly int amount = 20;
    private int selectedPage = 1;

    protected override async Task OnParametersSetAsync()
    {
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page = Page,
            SearchTerm = SearchValue,
            Amount = amount,
        });
        hosts = response.Hosts;
        totalHosts = response.TotalAmount;
        totalPages = totalHosts / amount + (totalHosts % amount > 0 ? 1 : 0);
        selectedPage = Page>0?Page:1;
    }

    private async Task ClickHandler(int pageNr)
    {
        Page = pageNr;
        HostResponse.GetIndex response = await HostService.GetIndexAsync(new HostRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm = SearchValue
        });
        hosts = response.Hosts;
        selectedPage = pageNr;

        Dictionary<string, object> parameters = new()
        {
            {nameof(Page), Page }
        };
        if (!string.IsNullOrEmpty(SearchValue))
        {
            parameters.Add(nameof(SearchValue), SearchValue);
        }
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);

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
        Page = selectedPage;
        NavigationManager.NavigateTo("host/list");
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
        selectedPage = 1;
        Dictionary<string, object> parameters = new()
        {
            {nameof(Page), 1 },
            {nameof(SearchValue), SearchValue }
        };
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);

        StateHasChanged();
    }
}