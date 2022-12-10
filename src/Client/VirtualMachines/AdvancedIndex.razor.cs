using Microsoft.AspNetCore.Components;
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

    private VirtualMachineDto.Index selectedItem1 = null;
    private IEnumerable<VirtualMachineDto.Index> virtualMachines = new List<VirtualMachineDto.Index>();

    [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;


    protected async override Task OnInitializedAsync()
    {
        VirtualMachineResponse.GetIndex response = await VirtualMachineService.GetIndexAsync(new VirtualMachineRequest.GetIndex()) ?? new VirtualMachineResponse.GetIndex();
        virtualMachines = response.VirtualMachines;
    }

    private bool FilterFunc1(VirtualMachineDto.Index element) => FilterFunc(element, searchString1);

    private bool FilterFunc(VirtualMachineDto.Index element, string searchString)
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
