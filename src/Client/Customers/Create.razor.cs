using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Customer;

namespace Client.Customers;

public partial class Create
{
    //Model
    private CustomerDto.Mutate Customer { get; set; } = new();

    [Inject] public ICustomerService? CustomerService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }


    private EditForm? Editform { get; set; } = new();
    private List<string> _typesClient = Enum.GetNames(typeof(CustomerType)).ToList();
    private List<string> _institution = Enum.GetNames(typeof(Institution)).ToList();
    private string _customcss = "background-color: white";
    private bool _backuprequired = false;
    private string[] _values = new string[3] { "", "", "" };

    public void Makerequired(string value)
    {
        int index = int.Parse(value.Substring(0, 1));
        string txt = value.Substring(1);
        _values[index] = txt;
        _backuprequired = _values.All(e => (e == "" || e == null)) ? false : true;
    }

    private async void HandleValidSubmit()
    {
        int newID = await CustomerService!.Add(Customer);
        Navigation!.NavigateTo("/customer/" + newID);
    }
}