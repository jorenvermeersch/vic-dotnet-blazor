
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Accounts;
using Shared.VirtualMachines;

namespace Client.Accounts;

public partial class Details
{
    private AccountDto.Detail? account;

    [Inject] public IAccountService AccountService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IStringLocalizer<SharedFiles.Resources.Resource> Localizer { get; set; } = default!;

    [Parameter] public long Id { get; set; }

    private IEnumerable<VirtualMachineDto.Index>? virtualMachines;
    private int offset, totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;

    private Dictionary<string, string>? username = new();
    private Dictionary<string, string>? generalInformation = new();
    private Dictionary<string, string>? contactInformation = new();

    protected override async Task OnInitializedAsync()
    {
        AccountResponse.GetDetail response = await AccountService.GetDetailAsync(new AccountRequest.GetDetail() { AccountId = Id }) ?? new AccountResponse.GetDetail();
        account = response.Account;
        generalInformation?.Add("Role", Localizer[account!.Role.ToString()]);
        username?.Add("Naam", string.Concat(account!.Firstname, " ", account!.Lastname));
        generalInformation?.Add("Departement", account!.Department);
        generalInformation?.Add("Opleiding", account!.Education);
        contactInformation?.Add("E-mailadres", account!.Email);
        //VirtualMachineResponse.GetIndex vmresponse = await virtualMachineService.GetVirtualMachinesByAccountId(new VirtualMachineRequest.GetByObjectId{ObjectId = account.Id});
        //virtualMachines = vmresponse.VirtualMachines;
        //totalVirtualMachines = vmresponse.TotalAmount;
        //totalPages = (totalVirtualMachines / 10) + 1;
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("account/list");
    }
}