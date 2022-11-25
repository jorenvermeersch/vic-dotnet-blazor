using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Host;

namespace Client.Hosts;

public partial class Create
{
    [Inject] public NavigationManager? Navigation { get; set; }
    [Inject] public IHostService? HostService { get; set; }


    private EditForm? Editform { get; set; } = new();
    private HostDto.Mutate Host { get; set; } = new();
    private async void HandleValidSubmit()
    {
        HostDto.Detail newHost = await HostService!.Add(Host);
        Navigation!.NavigateTo("host/" + newHost.Id);
    }
}