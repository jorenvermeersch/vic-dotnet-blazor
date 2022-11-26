using Microsoft.AspNetCore.Components;
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
    }

    private void FilterExtern()
    {
        toggleExtern = !toggleExtern;
    }

    private void ResetFilter()
    {
        toggleIntern = toggleExtern = false;
        SearchValue = "";
    }

    protected override async Task OnInitializedAsync()
    {
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Offset = offset
        });
        customers = response.Customers;
        totalCustomers = response.TotalAmount;
        totalPages = (totalCustomers / 20) + 1;
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        CustomerResponse.GetIndex response = await CustomerService.GetIndexAsync(new CustomerRequest.GetIndex
        {
            Offset = offset
        });
        customers = response.Customers;
        selectedPage = pageNr;
    }
}