
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Account;
using Shared.VirtualMachine;

namespace Client.Accounts;

public partial class Details
{
    //MODEL
    private AccountDto.Details? account;

    [Inject] public IAccountService? AccountService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }
    [Inject] public IStringLocalizer<Shared.Resources.Resource>? localizer { get; set; }



    [Parameter] public long Id { get; set; }

    private IEnumerable<VirtualMachineDto.Index>? virtualMachines;
    int offset, totalVirtualMachines, totalPages = 0;
    int selectedPage = 1;
    private Dictionary<string, string>? _username = new();
    private Dictionary<string, string>? _general = new();
    private Dictionary<string, string>? _contactInformation = new();

    protected override async Task OnInitializedAsync()
    {
        AccountRequest.GetDetail request = new() { AccountId = (int)Id };
        AccountResult.GetDetail response = await AccountService!.GetDetailAsync(request);
        account = response.Account;
        _general?.Add("Role", localizer![account!.Role.ToString()]);
        _username?.Add("Naam", string.Concat(account!.Firstname, " ", account!.Lastname));
        _general?.Add("Departement", account!.Department);
        _general?.Add("Opleiding", account!.Education);
        _contactInformation?.Add("E-mailadres", account!.Email);
        //VirtualMachineResponse.GetIndex vmresponse = await virtualMachineService.GetVirtualMachinesByAccountId(new VirtualMachineRequest.GetByObjectId{ObjectId = account.Id});
        //virtualMachines = vmresponse.VirtualMachines;
        //totalVirtualMachines = vmresponse.TotalAmount;
        //totalPages = (totalVirtualMachines / 10) + 1;
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("account/list");
    }

    private async Task ClickHandler(int pageNr)
    {
        return;
        //offset = (pageNr - 1) * 10;
        //virtualMachines = await virtualMachineService.GetVirtualMachinesByAccountId(account.Id, offset);
        //selectedPage = pageNr;
    }
}