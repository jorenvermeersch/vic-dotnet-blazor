using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
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

    private bool backupRequired = false;
    private string[] backupContactValues = new string[3] { "", "", "" };

    private void UpdateBackupContactRequired(string? value, int index)
    {
        backupContactValues[index] = value ?? "";
        backupRequired = backupContactValues.Any(value => !value.IsNullOrEmpty());
    }

    private async void HandleValidSubmit()
    {
        CustomerRequest.Create request = new() { Customer = Customer };
        var response = await CustomerService!.CreateAsync(request);
        Navigation.NavigateTo("customer/" + response.CustomerId);
    }
}