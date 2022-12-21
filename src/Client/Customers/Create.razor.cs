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

    private Dictionary<string, string> translatedCustomerTypes = new();
    private Dictionary<string, string> translatedInstitutions = new();

    private bool backupRequired = false;
    private string?[] backupContactValues = new string?[4] { "", "", "", "" };

    protected override void OnInitialized()
    {
        TranslateCustomerTypes();
        TranslateInstitutions();
    }

    private void UpdateBackupContactRequired(string? value, int index)
    {
        backupContactValues[index] = value;
        backupRequired = backupContactValues.Any(value => !value.IsNullOrEmpty());
    }

    private async void HandleValidSubmit()
    {
        if (!AnyFieldsFilledIn(Customer.BackupContactPerson))
        {
            Customer.BackupContactPerson = null;
        }

        CustomerRequest.Create request = new() { Customer = Customer };
        var response = await CustomerService.CreateAsync(request);
        Navigation.NavigateTo($"customer/{response.CustomerId}");

    }

    private bool AnyFieldsFilledIn(ContactPersonDto? contactPerson)
    {
        if (contactPerson is null) return false;
        string?[] fields = new string?[4] { contactPerson.Firstname, contactPerson.Lastname, contactPerson.Email, contactPerson.Phonenumber };
        return fields.Any(field => !field.IsNullOrEmpty());
    }

    // Functions and set-up required for drop-down. 
    private void TranslateCustomerTypes()
    {
        List<string> customerTypes = Enum.GetNames(typeof(CustomerType)).ToList();
        customerTypes.ForEach(customerType => translatedCustomerTypes.Add(customerType, customerType));
    }

    private void SetCustomerType(string typeString)
    {
        bool success = Enum.TryParse(typeString, out CustomerType type);
        if (success) Customer.CustomerType = type;
    }

    private void TranslateInstitutions()
    {
        List<string> institutions = Enum.GetNames(typeof(Institution)).ToList();
        institutions.ForEach(institution => translatedInstitutions.Add(institution, institution));
    }

    private void SetInstitution(string institutionString)
    {
        bool success = Enum.TryParse(institutionString, out Institution institution);
        if (success) Customer.Institution = institution;
    }


}