﻿using Microsoft.AspNetCore.Components;
using Shared.VirtualMachines;
using static MudBlazor.CategoryTypes;

namespace Client.VirtualMachines;

public partial class AdvancedIndex
{
    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string searchString1 = "";

    private VirtualMachineDto.Detail selectedItem1 = null;
    private IEnumerable<VirtualMachineDto.Detail> virtualMachines = new List<VirtualMachineDto.Detail>();

    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;


    protected async override Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetAllDetails response = await VirtualMachineService.GetAllDetailsAsync(new VirtualMachineRequest.GetAllDetails()) ?? new VirtualMachineResponse.GetAllDetails();
        virtualMachines = response.VirtualMachines;
    }

    private bool FilterFunc1(VirtualMachineDto.Detail element) => FilterFunc(element, searchString1);

    private bool FilterFunc(VirtualMachineDto.Detail element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.Fqdn.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

}
