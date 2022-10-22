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

namespace Client.Components
{
    public partial class DataCard
    {
        [Parameter]
        public IDictionary<string, string> Entries { get; set; } = new Dictionary<string, string>();

        public DataCard()
        {
            Entries.Add("Email", "joren.vermeersch@student.hogent.be");
            Entries.Add("Telefoonnummer", "09 325 05 27"); 
        }
    }
}