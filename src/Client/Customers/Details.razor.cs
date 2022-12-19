using Client.Extensions;
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

    private Dictionary<string, Dictionary<string, string>> datacards = new();
    private string USERNAME_KEY = "USERNAME";
    private string GENERAL_INFORMATION_KEY = "GENERAL_INFORMATION";
    private string CONTACT_KEY = "CONTACT";
    private string BACKUP_CONTACT_KEY = "BACKUP_CONTACT";

    private IEnumerable<VirtualMachineDto.Index> virtualMachines = new List<VirtualMachineDto.Index>();
    private int offset = 0, totalVirtualMachines, totalPages = 0;
    private int selectedPage = 1;



    protected override async Task OnInitializedAsync()
    {
        var response = await CustomerService.GetDetailAsync(new CustomerRequest.GetDetail()
        {
            CustomerId = Id
        });
        customer = response.Customer;

        // General information. 
        datacards = new()
        {
            {
                USERNAME_KEY,
                new()
                {
                    { "Naam", customer.GetFullName() }
                }
            },
            {
                CONTACT_KEY,
                new()
                {
                    { "Naam", customer.ContactPerson.GetFullName() },
                    { "E-mailadres", customer.ContactPerson.Email.FormatIfEmpty() },
                    { "Telefoonnummer", customer.ContactPerson.Phonenumber.FormatIfEmpty() }
                }
            },
        };

        // Information based on customer type. 
        if (customer.IsInternal())
        {
            datacards.Add(
                GENERAL_INFORMATION_KEY,
                new()
                {
                    { "Soort", customer.CustomerType.ToString() },
                    { "Instituut", customer.Institution.ToString()! },
                    { "Departement", customer.Department! },
                    { "Opleiding", customer.Education! }
                }
            );

        }
        else
        {
            datacards.Add(
               GENERAL_INFORMATION_KEY,
               new()
               {
                   { "Soort", customer.CustomerType.ToString() },
                   { "Naam", customer.CompanyName! },
                   { "Type", customer.CompanyType! },
               }
           );
        }

        // Back-up contact information. 
        if (customer.BackupContactPerson is not null)
        {
            datacards.Add(
                BACKUP_CONTACT_KEY,
                new()
                {
                    { "Naam", customer.BackupContactPerson.GetFullName() },
                    { "E-mailadres", customer.BackupContactPerson.Email.FormatIfEmpty() },
                    { "Telefoonnummer", customer.BackupContactPerson.Phonenumber.FormatIfEmpty() }
                }
            );
        }

        // TODO: Properly do virtual machines. 

        virtualMachines = customer.VirtualMachines;
        totalVirtualMachines = customer.VirtualMachines.Count;
        totalPages = (totalVirtualMachines / 10) + 1;
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
