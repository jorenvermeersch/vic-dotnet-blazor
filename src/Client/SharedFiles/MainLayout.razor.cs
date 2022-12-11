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
using Client.Authentication;
using System.ComponentModel;
using Domain;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Client.SharedFiles.Resources;
using Microsoft.Extensions.Localization;
using Blazored.FluentValidation;
using Shared.VirtualMachines;
using MudBlazor;
using Client.Components;

namespace Client.SharedFiles
{
    public partial class MainLayout
    {
        private string _currentUrl = "";
        protected override void OnInitialized()
        {
            _currentUrl = NavigationManager.Uri;
        }
    }
}