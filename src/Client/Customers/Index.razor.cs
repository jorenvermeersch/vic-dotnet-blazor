using Microsoft.AspNetCore.Components;
using Services.Customers;
using Shared.Customers;

namespace Client.Customers;

public partial class Index
{
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public string? SearchValue { get; set; }
    [Parameter, SupplyParameterFromQuery] public string? Type { get; set; }
    [Parameter, SupplyParameterFromQuery] public int Page { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<CustomerDto.Index>? customers;
    private int totalCustomers = 0, totalPages = 0;
    private int selectedPage = 1;
    private bool toggleIntern, toggleExtern = false;
    private readonly int amount = 20;

    protected override async Task OnParametersSetAsync()
    {
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Page = 1,
            SearchTerm = SearchValue,
            CustomerType = Type,
            Amount = amount,
        });
        customers = response.Customers;
        totalCustomers = response.TotalAmount;
        totalPages = totalCustomers / amount + (totalCustomers % amount > 0 ? 1 : 0);
        selectedPage = Page > 0 ? Page : 1;

    }

    private void FilterIntern()
    {
        toggleIntern = !toggleIntern;
        toggleExtern = false;

    }

    private void FilterExtern()
    {
        toggleExtern = !toggleExtern;
        toggleIntern = false;
    }


    private async void ResetFilter()
    {
        toggleIntern = false;
        toggleExtern = false;
        SearchValue = "";
        Type = "";
        CustomerRequest.GetIndex request = new()
        {
            Page = 1,
            Amount = amount,
        };
        var response = await CustomerService.GetIndexAsync(request);
        totalCustomers = response.TotalAmount;
        customers = response.Customers;
        totalPages = totalCustomers / amount + (totalCustomers % amount > 0 ? 1 : 0);
        selectedPage = 1;
        Page = selectedPage;
        NavigationManager.NavigateTo("customer/list");
        StateHasChanged();
    }
    private async void HandleFilter()
    {
        CustomerRequest.GetIndex request = new()
        {
            Page = 1,
            Amount = amount,
            SearchTerm = SearchValue,
        };

        if (toggleIntern)
        {
            request.CustomerType = "intern";
        }
        else if (toggleExtern)
        {
            request.CustomerType = "extern";
        }

        var response = await CustomerService.GetIndexAsync(request);
        totalCustomers = response.TotalAmount;
        customers = response.Customers;
        totalPages = totalCustomers / amount + (totalCustomers % amount > 0 ? 1 : 0);
        selectedPage = 1;
        Type = request.CustomerType;
        Dictionary<string, object> parameters = new()
        {
            {nameof(Page), 1 },
            {nameof(SearchValue), SearchValue },
            {nameof(Type), Type }
        };
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);

        StateHasChanged();
    }


    private async Task ClickHandler(int pageNr)
    {
        Page = pageNr;
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm= SearchValue,
            CustomerType = Type,
        });
        customers = response.Customers;
        selectedPage = pageNr;

        Dictionary<string, object> parameters = new()
        {
            {nameof(Page), Page }
        };
        if (!string.IsNullOrEmpty(SearchValue))
        {
            parameters.Add(nameof(SearchValue), SearchValue);
        }
        if (!string.IsNullOrEmpty(Type))
        {
            parameters.Add(nameof(Type), Type);
        }
        var uri = NavigationManager.GetUriWithQueryParameters(parameters);
        NavigationManager.NavigateTo(uri);
    }
}