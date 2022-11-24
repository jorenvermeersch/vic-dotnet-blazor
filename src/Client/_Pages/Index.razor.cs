using System;
using Microsoft.AspNetCore.Components;
using Shared.VirtualMachine;
using Microsoft.Extensions.Localization;

namespace Client._Pages;

public partial class Index
{
    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; } 
    [Inject] public IStringLocalizer<Client.Shared.Resources.Resource> localizer { get; set; } 

    string _startLabel = "24/10", _endLabel = "30/10";
    private List<VirtualMachineDto.Index> virtualMachines = new();
    int offset = 0, totalVirtualMachines, totalPages = 0;
    int selectedPage = 1;


    protected override async Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetAllUnfinishedVirtualMachines(new VirtualMachineRequest.GetIndex());
        virtualMachines.AddRange(response.VirtualMachines);
        totalVirtualMachines = response.TotalAmount;
        totalPages = (totalVirtualMachines / 10) + 1;
    }

    async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        VirtualMachineResponse.GetIndex response = await VirtualMachineService!.GetAllUnfinishedVirtualMachines(new VirtualMachineRequest.GetIndex());
        virtualMachines.AddRange(response.VirtualMachines);
        selectedPage = pageNr;
    }
}