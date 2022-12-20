using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;

namespace Client._Pages;

public partial class Index
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; } = default!;
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;

    private string _startLabel = "24/10", _endLabel = "30/10";
    private List<VirtualMachineDto.Index>? virtualMachines;
    private int totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;
    private readonly int amount = 10;

    private bool FetchingResources()
    {
        return virtualMachines is null;
    }

    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = selectedPage,
            IsUnfinished = true,
            Amount = amount
        });
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = totalVirtualMachines / amount + (totalVirtualMachines % amount > 0 ? 1 : 0);
    }

    private async Task ChangePage(int pageNr)
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            IsUnfinished = true
        });
        virtualMachines = response.VirtualMachines;
        selectedPage = pageNr;
    }
}