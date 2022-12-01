using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Accounts;


namespace Client.Accounts;

public partial class Create
{
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
        Console.WriteLine(Account.Role);
        Account.Role = localizer[Account.Role];
        AccountRequest.Create request = new()
        {
            Account = Account
        };
        AccountResponse.Create response = await AccountService!.CreateAsync(request);
        Navigation?.NavigateTo("account/" + response.AccountId);
    }
}