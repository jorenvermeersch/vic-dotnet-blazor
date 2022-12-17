using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Shared.Customers;

namespace Client.Customers;

public partial class Create
{
    [Parameter] public long Id { get; set; } = default!;
    private CustomerDto.Mutate Customer { get; set; } = new();

    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;


    private List<string> customerTypes = Enum.GetNames(typeof(CustomerType)).ToList();
    private List<string> institutions = Enum.GetNames(typeof(Institution)).ToList();

    private string _customcss = "background-color: white";
    private bool backupRequired = false;
    private string[] _values = new string[3] { "", "", "" };
    public void MakeRequired(string value)
    {
        int index = int.Parse(value.Substring(0, 1));
        string txt = value.Substring(1);
        _values[index] = txt;
        backupRequired = _values.All(e => (e == "" || e == null)) ? false : true;
    }

    private async void HandleValidSubmit()
    {
        CustomerRequest.Create request = new() { Customer = Customer };
        var response = await CustomerService!.CreateAsync(request);
        Navigation.NavigateTo("customer/" + response.CustomerId);
    }
}