
using Client.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Accounts;

namespace Client.Accounts;

public partial class Details
{
    private AccountDto.Detail? account;

    [Inject] public IAccountService AccountService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IStringLocalizer<SharedFiles.Resources.Resource> Localizer { get; set; } = default!;

    [Parameter] public long Id { get; set; }

    private Dictionary<string, Dictionary<string, string>> datacards = new();
    private string USERNAME_KEY = "USERNAME";
    private string GENERAL_INFORMATION_KEY = "GENERAL_INFORMATION";
    private string CONTACT_INFORMATION_KEY = "CONTACT_INFORMATION";

    protected override async Task OnInitializedAsync()
    {
        AccountResponse.GetDetail response = await AccountService.GetDetailAsync(
            new AccountRequest.GetDetail()
            {
                AccountId = Id
            }
        );
        account = response.Account;

        datacards = new()
        {
             {
                USERNAME_KEY,
                new()
                {
                    { "Naam", account!.GetFullName() },
                }
            },
            {
                GENERAL_INFORMATION_KEY,
                new()
                {
                    { "Rol", Localizer[account!.Role.ToString()] },
                    { "Departement", account!.Department },
                    { "Opleiding", account!.Education.FormatIfEmpty() },
                }
            },
            {
                CONTACT_INFORMATION_KEY,
                new()
                {
                    { "E-mailadres", account!.Email },
                }
            }
        };

        // TODO: Fetch virtual machines of admin. 

    }

    private void NavigateBack()
    {
        Navigation!.NavigateTo("account/list");
    }
}