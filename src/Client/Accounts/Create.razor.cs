using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Accounts;


namespace Client.Accounts;

public partial class Create
{
    //Model
    private AccountDto.Mutate Account { get; set; } = new();

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
        AccountResponse.Create response = await AccountService!.CreateAsync(new AccountRequest.Create
        {
            Account = Account,
        });
        Navigation?.NavigateTo("account/" + response.AccountId);
    }
}