using Microsoft.AspNetCore.Components;
using Shared.Account;

namespace Client.Account;

public partial class Index
{
    [Inject] public IAccountService? AccountService { get; set; }

    public string? SearchValue { get; set; }
    private List<AccountDto.Index>? accounts;
    int offset, totalaccounts, totalPages = 0;
    int selectedPage = 1;
    bool toggleAdmin, toggleObserver, toggleMaster;

    protected override async Task OnInitializedAsync()
    {
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex());
        accounts = response.Accounts;
        totalaccounts = response.TotalAmount;
        totalPages = (totalaccounts / 20) + 1;

        Console.WriteLine(response.Accounts[0].Firstname);
    }

    async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex());
        accounts = response.Accounts;
        selectedPage = pageNr;
    }

    private void FilterAdmin() => toggleAdmin = !toggleAdmin;
    private void FilterObserver() => toggleObserver = !toggleObserver;
    private void FilterMaster() => toggleMaster = !toggleMaster;

    private void ResetFilter()
    {
        SearchValue = "";
        toggleAdmin = toggleMaster = toggleObserver = false;
    }
}