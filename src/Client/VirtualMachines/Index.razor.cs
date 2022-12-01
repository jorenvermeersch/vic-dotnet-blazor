using Azure;
using Microsoft.AspNetCore.Components;
using Shared.VirtualMachines;
using System.Drawing.Printing;

namespace Client.VirtualMachines;

public partial class Index
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; }

    public string? SearchValue { get; set; }

    private List<VirtualMachineDto.Index>? virtualMachines;
    private int totalVirtualMachines = 0;
    private int totalPages;
    private int selectedPage = 1;
    private readonly int amount=20;
    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page=1,
            Amount=amount,
        });
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = totalVirtualMachines / amount + (totalVirtualMachines % amount > 0 ? 1 : 0);
        selectedPage = 1;
    }

    private async Task ClickHandler(int pageNr)
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm = SearchValue
        });
        virtualMachines = response.VirtualMachines;
        selectedPage = pageNr;
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
        StateHasChanged();
    }
}