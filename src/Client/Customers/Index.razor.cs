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
    private int offset = 0, totalCustomers = 0, totalPages = 0;
    private int selectedPage = 1;
    private string CustomerType = "";
    private bool toggleIntern, toggleExtern = false;

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
            Page = 0,
            Amount = 20,
        };
        var response = await CustomerService.GetIndexAsync(request);
        totalCustomers = response.TotalAmount;
        customers = response.Customers;
        totalPages = Convert.ToInt16(Math.Ceiling(totalCustomers / 20.0));
        StateHasChanged();
    }
    private async void HandleFilter()
    {
        CustomerRequest.GetIndex request = new()
        {
            Page = 0,
            Amount = 20,
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
        totalPages = Convert.ToInt16(Math.Ceiling(totalCustomers / 20.0));
        StateHasChanged();
    }

    protected override async Task OnParametersSetAsync()
    {
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
           Page = 0
        });
        customers = response.Customers;
        totalCustomers = response.TotalAmount;
        totalPages = Convert.ToInt16(Math.Ceiling(totalCustomers / 20.0));
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Page = pageNr,
            SearchTerm= SearchValue,
        });
        customers = response.Customers;
        selectedPage = pageNr;
    }
}