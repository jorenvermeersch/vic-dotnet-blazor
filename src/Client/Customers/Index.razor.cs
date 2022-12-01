using Microsoft.AspNetCore.Components;
using Services.Customers;
using Shared.Customers;

namespace Client.Customers;

public partial class Index
{
    [Inject] public ICustomerService CustomerService { get; set; } = default!;

    public string SearchValue { get; set; } = "";

    //Alle Customers
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
            Amount= amount,
        });
        customers = response.Customers;
        totalCustomers = response.TotalAmount;
        totalPages = totalCustomers / amount + (totalCustomers % amount > 0 ? 1 : 0);
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
        StateHasChanged();
    }


    private async Task ClickHandler(int pageNr)
    {
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Page = pageNr,
            Amount = amount,
            SearchTerm= SearchValue,
        });
        customers = response.Customers;
        selectedPage = pageNr;
    }
}