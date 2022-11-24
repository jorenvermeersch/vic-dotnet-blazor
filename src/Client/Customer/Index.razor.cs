using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Shared.Customer;


namespace Client.Customer;

public partial class Index
{
    [Inject] public ICustomerService CustomerService { get; set; }

    public string SearchValue { get; set; }

    //Alle Customers
    private IEnumerable<CustomerDto.Index>? customers;

    int offset = 0, totalCustomers = 0, totalPages = 0;
    int selectedPage = 1;
    string CustomerType = "";
    bool toggleIntern, toggleExtern = false;

    private void FilterIntern() => toggleIntern = !toggleIntern;
    private void FilterExtern() => toggleExtern = !toggleExtern;

    private void ResetFilter()
    {
        toggleIntern = toggleExtern = false;
        SearchValue = "";
    }

    protected override async Task OnInitializedAsync()
    {
        customers = await CustomerService.GetIndexAsync(offset);
        totalCustomers = await CustomerService.GetCount();
        totalPages = (totalCustomers / 20) + 1;
    }

    async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 20;
        customers = await CustomerService.GetIndexAsync(offset);
        selectedPage = pageNr;
    }
}