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
    [Inject] public IStringLocalizer<SharedFiles.Resources.Resource> Localizer { get; set; } = default!;


    private List<string> roles = Enum.GetNames(typeof(Role)).ToList();
    protected override void OnInitialized()
    {
        for (int i = 0; i < roles.Count; i++) roles[i] = Localizer[roles[i]];
    }
    protected override async Task OnParametersSetAsync()
    {
        if (Convert.ToBoolean(Id))
        {
            AccountResponse.GetDetail response = await AccountService.GetDetailAsync(new AccountRequest.GetDetail
            {
                AccountId = Id
            });
            Account = new AccountDto.Mutate
            {
                Firstname = response.Account.Firstname,
                Lastname = response.Account.Lastname,
                Department = response.Account.Department,
                Education = response.Account.Education,
                Email = response.Account.Email,
                IsActive = response.Account.IsActive,
                Role = Localizer[response.Account.Role.ToString()]
            };
        }
    }

    private async void HandleValidSubmit()
    {
        Account.Role = Localizer[Account.Role];
        if (Convert.ToBoolean(Id))
        {
            AccountRequest.Edit request = new()
            {
                AccountId = Id,
                Account = Account
            };
            var response = await AccountService.EditAsync(request);
            Navigation.NavigateTo($"account/{response.AccountId}");
        }
        else
        {

            AccountRequest.Create request = new()
            {
                Account = Account
            };
            AccountResponse.Create response = await AccountService!.CreateAsync(request);
            Navigation!.NavigateTo("account/" + response.AccountId);
        }

    }
}