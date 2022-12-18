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


    private Dictionary<string, string> translatedRoles = new();

    protected override void OnInitialized()
    {
        TranslateRoleOptions();
    }
    private async void HandleValidSubmit()
    {
        AccountRequest.Create request = new()
        {
            Account = Account
        };
        AccountResponse.Create response = await AccountService!.CreateAsync(request);
        Navigation!.NavigateTo("account/" + response.AccountId);
    }

    private void TranslateRoleOptions()
    {
        List<string> roles = Enum.GetNames(typeof(Role)).ToList();
        roles.ForEach(role => translatedRoles.Add(Localizer[role], role));
    }

    private void SetRole(string roleString)
    {
        bool success = Enum.TryParse(roleString, out Role role);
        if (success) Account.Role = role;
    }
}