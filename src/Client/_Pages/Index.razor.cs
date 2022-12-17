using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;

namespace Client._Pages;

public partial class Index
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; }
    [Inject] public IStringLocalizer<Client.SharedFiles.Resources.Resource> localizer { get; set; }

    private string _startLabel = "24/10", _endLabel = "30/10";
    private List<VirtualMachineDto.Index> virtualMachines = new();
    private int totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;
    private readonly int amount = 10;

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