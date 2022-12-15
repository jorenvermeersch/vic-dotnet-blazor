using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Shared.Accounts;

namespace Client.Accounts;

public partial class Index
{
    [Inject] public IAccountService? AccountService { get; set; }

    [Parameter, SupplyParameterFromQuery] public string SearchValue { get; set; } = "";
    [Parameter, SupplyParameterFromQuery] public int Page { get; set; } = 1;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private List<AccountDto.Index>? accounts;
    private int totalAccounts, totalPages = 0;
    private int selectedPage = 1;
    private int amount = 20;
    private bool toggleAdmin, toggleObserver, toggleMaster;
    private List<string> filterRoles;

    protected override async Task OnParametersSetAsync()
    {
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page=Page,
            Amount = amount,
            SearchTerm = SearchValue,
        });
        accounts = response.Accounts;
        totalAccounts = response.TotalAmount;
        totalPages = totalAccounts / amount + (totalAccounts % amount > 0 ? 1 : 0);
        selectedPage = Page > 0 ? Page : 1;
    }

    private async Task ClickHandler(int pageNr)
    {
        Page = pageNr;
        AccountResponse.GetIndex response = await AccountService!.GetIndexAsync(new AccountRequest.GetIndex
        {
            Page=pageNr,
            Amount = amount,
            SearchTerm = SearchValue,
            Roles = filterRoles
        });
        accounts = response.Accounts;
        selectedPage = pageNr;

        HandleNavigation();
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
        //Roles = null;
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
        Page = selectedPage;
        NavigationManager.NavigateTo("account/list");
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
        Page = 1;

        HandleNavigation();
    }

    private void HandleNavigation()
    {
        Dictionary<string, object> parameters = new()
        {
            {nameof(Page), Page },
            {nameof(SearchValue), SearchValue },
        };
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);

        StateHasChanged();
    }
}