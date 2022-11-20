using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Client;
using Client.Shared;
using System.ComponentModel;
using Domain;
using Domain.Constants;
using Client.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Client.Shared.Resources;
using Microsoft.Extensions.Localization;
using Shared.Account;

namespace Shared
{
    public partial class IndexAccount
    {
        [Inject] public IAccountService AccountService { get; set; }

        public string SearchValue { get; set; }


        private IEnumerable<AccountDto.Index>? accounts;
        int offset = 0;
        int totalaccounts = 0;
        int totalPages = 0;
        int selectedPage = 1;
        bool toggleAdmin;
        bool toggleObserver;
        bool toggleMaster;

        protected override async Task OnInitializedAsync()
        {
            AccountResponse.GetIndex response = await AccountService.GetIndexAsync(new AccountRequest.GetIndex());
            accounts = response.Accounts;
            totalaccounts = response.TotalAmount;
            totalPages = (totalaccounts / 20) + 1;
        }

        async Task ClickHandler(int pageNr)
        {
            offset = (pageNr - 1) * 20;
            AccountResponse.GetIndex response = await accountService.GetIndexAsync(new AccountRequest.GetIndex());
            accounts = response.Accounts;
            selectedPage = pageNr;
        }

        private void FilterAdmin()
        {
            toggleAdmin = !toggleAdmin;
        }

        private void FilterObserver()
        {
            toggleObserver = !toggleObserver;
        }

        private void FilterMaster()
        {
            toggleMaster = !toggleMaster;
        }

        private void ResetFilter()
        {
            SearchValue = "";
            toggleAdmin = false;
            toggleMaster = false;
            toggleObserver = false;
            StateHasChanged();
        }
    }
}