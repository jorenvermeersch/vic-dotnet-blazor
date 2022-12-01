using Azure;
using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Shared.Accounts;

namespace Client.Accounts;

public partial class Index
{
    [Inject] public IAccountService? AccountService { get; set; }

    public string? SearchValue { get; set; }
    private List<AccountDto.Index>? accounts;
    private int offset, totalAccounts, totalPages = 0;
    private int selectedPage = 1;
    private int amount = 20;
    private bool toggleAdmin, toggleObserver, toggleMaster;
    private List<string> filterRoles;

    protected override async Task OnInitializedAsync()
    {
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page=1,
            Amount = amount,
        });
        accounts = response.Accounts;
        totalAccounts = response.TotalAmount;
        totalPages = totalAccounts / amount + (totalAccounts % amount > 0 ? 1 : 0);
    }

    private async Task ClickHandler(int pageNr)
    {
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page=pageNr,
            Amount = amount,
            SearchTerm = SearchValue,
            Roles = filterRoles
        });
        accounts = response.Accounts;
        selectedPage = pageNr;
    }

    private void FilterAdmin()
    {
        toggleAdmin = !toggleAdmin;
    }

    private void FilterObserver()
    {
        toggleObserver = !toggleObserver;
    }

    private void FilterMaster()
    {
        toggleMaster = !toggleMaster;
    }

    private async void ResetFilter()
    {
        SearchValue = "";
        toggleAdmin = toggleMaster = toggleObserver = false;
        filterRoles = null;
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page = 1,
            Amount = amount,
            SearchTerm = SearchValue,
            Roles = filterRoles
        }); 
        accounts = response.Accounts;
        totalAccounts = response.TotalAmount;
        totalPages = totalAccounts / amount + (totalAccounts % amount > 0 ? 1 : 0);
        selectedPage = 1;
        StateHasChanged();

    }
    private async void HandleFilter()
    {
        
        if(toggleAdmin || toggleMaster || toggleObserver)
        {
            filterRoles = new List<string>();
            if (toggleAdmin) filterRoles.Add(Role.Admin.ToString());
            if (toggleMaster) filterRoles.Add(Role.Master.ToString());
            if (toggleObserver) filterRoles.Add(Role.Observer.ToString());
        }
        

        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page = 1,
            Amount= amount,
            SearchTerm = SearchValue,
            Roles = filterRoles
        });

        accounts = response.Accounts;
        totalAccounts = response.TotalAmount;
        totalPages = totalAccounts / amount + (totalAccounts % amount > 0 ? 1 : 0);
        selectedPage = 1;
        StateHasChanged();
    }
}