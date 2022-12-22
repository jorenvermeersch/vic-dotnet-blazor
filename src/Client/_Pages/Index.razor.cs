using Client.Extensions;
using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.VirtualMachines;

namespace Client._Pages;

public partial class Index
{
    private List<VirtualMachineDto.Index>? virtualMachines;

    [Inject] public IVirtualMachineService? VirtualMachineService { get; set; } = default!;
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;

    // Mock data for BarGraph. 
    private int upperbound = 500;
    private string unit = "Aantal";
    private string startLabel = DateTime.UtcNow.FormatDate(format: "dd/MM");
    private string endLabel = DateTime.UtcNow.AddMonths(1).FormatDate(format: "dd/MM");

    // Pagination. 
    private int totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;
    private readonly int amount = 10;

    private bool FetchingResources()
    {
        return virtualMachines is null;
    }

    private List<int> GenerateGraphMockData()
    {
        List<int> data = new();

        Random random = new();
        for (int day = 1; day <= 18; day++)
        {
            data.Add(random.Next(upperbound));
        }
        return data;
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