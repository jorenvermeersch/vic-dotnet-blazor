using Client.Extensions;
using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Shared.Customers;
using Shared.VirtualMachines;

namespace Client.Customers;


public partial class Details
{
    private CustomerDto.Detail? customer;
    [Parameter] public long Id { get; set; }
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;




    private IEnumerable<VirtualMachineDto.Index> virtualMachines = new List<VirtualMachineDto.Index>();
    private int offset = 0, totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;


    private Dictionary<string, string> _username = new();
    private Dictionary<string, string> _general = new();
    private Dictionary<string, string> _contactInformation = new();
    private Dictionary<string, string> _backupContactInformation = new();

    protected override async Task OnInitializedAsync()
    {
        CustomerRequest.GetDetail request = new()
        {
            CustomerId = Id
        };
        var response = await CustomerService.GetDetailAsync(request);
        customer = response.Customer;


        virtualMachines = customer.VirtualMachines;
        totalVirtualMachines = customer.VirtualMachines.Count;
        totalPages = (totalVirtualMachines / 10) + 1;

        _username.Add("Naam", string.Concat(customer.ContactPerson.Firstname, " ", customer.ContactPerson.Lastname));
        _general.Add("Soort", customer.CustomerType.ToString());

        if (customer.CustomerType == CustomerType.Intern)
        {
            _general.Add("Instituut", customer.Institution.ToString()!);
            _general.Add("Departement", customer.Department!);
            _general.Add("Opleiding", customer.Education!);
        }
        else
        {
            _general.Add("Naam", customer.CompanyName!);
            _general.Add("Type", customer.CompanyType!);
        }

        _contactInformation.Add("Naam", string.Concat(customer.ContactPerson.Firstname, " ", customer.ContactPerson.Lastname));
        _contactInformation.Add("E-mailadres", customer.ContactPerson.Email.FormatIfEmpty());
        _contactInformation.Add("Telefoonnummer", customer.ContactPerson.Phonenumber.FormatIfEmpty());

        if (!string.IsNullOrEmpty(customer.BackupContactPerson?.Firstname))
        {
            _backupContactInformation.Add("Naam", string.Concat(customer.BackupContactPerson.Firstname, " ", customer.BackupContactPerson.Lastname));
            _backupContactInformation.Add("E-mailadres", customer.BackupContactPerson.Email.FormatIfEmpty());
            _backupContactInformation.Add("Telefoonnummer", customer.BackupContactPerson.Phonenumber.FormatIfEmpty());
        }
    }

    private async Task ChangePage(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        selectedPage = pageNr;
        // TODO: Implement. 
    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("customer/list");
    }
}
