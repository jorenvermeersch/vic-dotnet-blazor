using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Account;
using Client.Components;


namespace Client.Account;

public partial class Create
{
    //Model
    private AccountDto.Create Account { get; set; } = new();

    [Inject] public IAccountService? AccountService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }
    [Inject] public IStringLocalizer<Shared.Resources.Resource>? localizer { get; set; }


    private List<string> roles = Enum.GetNames(typeof(Role)).ToList();
    protected override void OnInitialized() 
    {
        for (int i = 0; i < roles.Count; i++) roles[i] = localizer![roles[i]];
    }

    private async void HandleValidSubmit()
    {
        AccountDto.Details newAccount = await AccountService!.Add(Account);
        Navigation?.NavigateTo("account/" + newAccount.Id);
    }
}