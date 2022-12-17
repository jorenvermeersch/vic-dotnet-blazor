using Client.SharedFiles.Resources;
using Domain.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Accounts;


namespace Client.Accounts;

public partial class Create
{
    [Parameter] public long Id { get; set; } = default!;
    private AccountDto.Mutate Account { get; set; } = new();

    [Inject] public IAccountService AccountService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IStringLocalizer<Resource> Localizer { get; set; } = default!;


    private List<string> roles = Enum.GetNames(typeof(Role)).ToList();
    private List<string> translatedRoles = new();
    protected override void OnInitialized()
    {
        // Translate roles. 
        foreach (var role in roles)
        {
            translatedRoles.Add(Localizer[role]);
        }
    }
    private async void HandleValidSubmit()
    {
        // Translate role back to stringified enum value. 
        int position = translatedRoles.FindIndex(role => role == Account.Role);
        Account.Role = Localizer[roles[position]];

        AccountRequest.Create request = new()
        {
            Account = Account
        };
        AccountResponse.Create response = await AccountService!.CreateAsync(request);
        Navigation!.NavigateTo("account/" + response.AccountId);


    }
}