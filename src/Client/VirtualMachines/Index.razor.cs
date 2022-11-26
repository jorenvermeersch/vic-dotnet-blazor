using Microsoft.AspNetCore.Components;
using Shared.VirtualMachine;

namespace Client.VirtualMachines;

public partial class Index
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; }

    public string? SearchValue { get; set; }

    private List<VirtualMachineDto.Index>? virtualMachines;
    private int offset = 0;
    private int totalVirtualMachines = 0;
    private int totalPages;
    private int selectedPage = 1;
    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex());
        virtualMachines = response.VirtualMachines;
        totalVirtualMachines = response.TotalAmount;
        totalPages = (totalVirtualMachines / 20) + 1;
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetIndexAsync(new VirtualMachineRequest.GetIndex());
        virtualMachines = response.VirtualMachines;
        //virtualMachines = await VirtualMachineService.GetIndexAsync(offset, 20);
        selectedPage = pageNr;
    }

    private void ResetFilter()
    {
        SearchValue = "";
    }
}