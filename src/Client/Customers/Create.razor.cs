using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Shared.Customers;

namespace Client.Customers;

public partial class Create
{
    [Parameter] public long Id { get; set; } = default!;
    private CustomerDto.Mutate Customer { get; set; } = new();

    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager? Navigation { get; set; }


    private List<string> _typesClient = Enum.GetNames(typeof(CustomerType)).ToList();
    private List<string> _institution = Enum.GetNames(typeof(Institution)).ToList();
    private string _customcss = "background-color: white";
    private bool _backuprequired = false;
    private string[] _values = new string[3] { "", "", "" };

    protected override async Task OnParametersSetAsync()
    {
        if (Convert.ToBoolean(Id))
        {
            CustomerResponse.GetDetail response = await CustomerService.GetDetailAsync(new CustomerRequest.GetDetail
            {
                CustomerId = Id
            });
            Customer = new CustomerDto.Mutate()
            {
                CustomerType = response.Customer.CustomerType.ToString(),
                Department = response.Customer.Department,
                Education = response.Customer.Education,
                Institution = response.Customer.Institution.ToString(),
                CompanyType = response.Customer.CompanyType,
                CompanyName = response.Customer.CompanyName,
                ContactPerson = response.Customer.ContactPerson,
                BackupContactPerson = response.Customer.BackupContactPerson
            };
        }
    }

    public void Makerequired(string value)
    {
        int index = int.Parse(value.Substring(0, 1));
        string txt = value.Substring(1);
        _values[index] = txt;
        _backuprequired = _values.All(e => (e == "" || e == null)) ? false : true;
    }

    private async void HandleValidSubmit()
    {
        if (Convert.ToBoolean(Id))
        {
            CustomerRequest.Edit request = new()
            {
                CustomerId = Id,
                Customer = Customer
            };
            var response =  await CustomerService.EditAsync(request);
            Navigation.NavigateTo($"customer/{response.CustomerId}");
        }
        else
        {
            CustomerRequest.Create request = new CustomerRequest.Create
            {
                Customer = Customer
            };
            var response = await CustomerService!.CreateAsync(request);

            Navigation!.NavigateTo("customer/" + response.CustomerId);
        }

    }
}