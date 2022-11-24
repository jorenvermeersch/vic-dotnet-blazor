using Microsoft.AspNetCore.Components;
using Domain.Constants;
using Shared.Customer;
using Shared.VirtualMachine;

namespace Client.Customer;

public partial class Details
{
    [Parameter] public long Id { get; set; }
    [Inject] public ICustomerService? CustomerService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }

    //Model
    private CustomerDto.Details? Customer;


    private IEnumerable<VirtualMachineDto.Index>? virtualMachines;
    int offset = 0, totalVirtualMachines, totalPages = 0;
    int selectedPage = 1;


    private Dictionary<string, string> _username = new();
    private Dictionary<string, string> _general = new();
    private Dictionary<string, string> _contactInformation = new();
    private Dictionary<string, string> _backupContactInformation = new();

    protected override async Task OnInitializedAsync()
    {
        Customer = await CustomerService.GetDetailAsync(Id);
        //virtualMachines = await virtualMachineService.GetVirtualMachinesByUserId(Id, offset);
        totalVirtualMachines = virtualMachines.Count();
        totalPages = (totalVirtualMachines / 10) + 1;
        _username.Add("Naam", string.Concat(Customer.ContactPerson.Firstname, " ", Customer.ContactPerson.Lastname));
        _general.Add("Klant type", Customer.CustomerType.ToString());
        if (Customer.CustomerType == CustomerType.Intern)
        {
            _general.Add("Instituut", Customer.Institution.ToString());
            _general.Add("Departement", Customer.Department);
            _general.Add("Opleiding", Customer.Education);
        }
        else
        {
            _general.Add("Naam", Customer.CompanyName);
            _general.Add("Type", Customer.CompanyType);
        }

        _contactInformation.Add("Naam", string.Concat(Customer.ContactPerson.Firstname, " ", Customer.ContactPerson.Lastname));
        _contactInformation.Add("E-mailadres", Customer.ContactPerson.Email);
        _contactInformation.Add("Telefoonnummer", Customer.ContactPerson.Phonenumber);
        if (Customer.BackupContactPerson.Firstname == "")
        {
            _backupContactInformation.Add("Naam", string.Concat(Customer.BackupContactPerson.Firstname, " ", Customer.BackupContactPerson.Lastname));
            _backupContactInformation.Add("E-mailadres", Customer.BackupContactPerson.Email);
            _backupContactInformation.Add("Telefoonnummer", Customer.BackupContactPerson.Phonenumber);
        }
    }

    private async Task ClickHandler(int pageNr)
    {
        offset = (pageNr - 1) * 10;
        //virtualMachines = await virtualMachineService.GetVirtualMachinesByUserId(customer.Id, offset);
        selectedPage = pageNr;
    }
    private void NavigateBack() => Navigation!.NavigateTo("customer/list");
}
