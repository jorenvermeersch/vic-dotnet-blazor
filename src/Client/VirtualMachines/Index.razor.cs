using Microsoft.AspNetCore.Components;
using Shared.VirtualMachines;

namespace Client.VirtualMachines;

public partial class Index
{
    private List<VirtualMachineDto.Index>? virtualMachines;

    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    // Filtering. 
    [Parameter, SupplyParameterFromQuery] public string? SearchValue { get; set; }
    [Parameter, SupplyParameterFromQuery] public int Page { get; set; }

    private int totalVirtualMachines = 0;
    private int totalPages;
    private int selectedPage = 1;
    private readonly int amount = 20;
    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = selectedPage,
            SearchTerm = SearchValue,
            Amount = amount,
        });
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = totalVirtualMachines / amount + (totalVirtualMachines % amount > 0 ? 1 : 0);
        selectedPage = Page > 0 ? Page : 1;
    }

    private void NavigateToTable()
    {
        NavigationManager.NavigateTo("/virtual-machine/table");
    }

    private async Task ClickHandler(int pageNr)
    {
        Page = pageNr;
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm = SearchValue
        });
        virtualMachines = response.VirtualMachines;
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
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = 1,
            Amount = amount,
            SearchTerm = SearchValue
        });
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = totalVirtualMachines / amount + (totalVirtualMachines % amount > 0 ? 1 : 0);
        selectedPage = 1;
        Page = selectedPage;
        NavigationManager.NavigateTo("virtual-machine/list");
        StateHasChanged();
    }

    private async void HandleFilter()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = 1,
            Amount = amount,
            SearchTerm = SearchValue
        });
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = totalVirtualMachines / amount + (totalVirtualMachines % amount > 0 ? 1 : 0);
        selectedPage = 1;
        Dictionary<string, object?> parameters = new()
        {
            {nameof(Page), 1 },
            {nameof(SearchValue), SearchValue }
        };
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);

        StateHasChanged();
    }
}