using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Create
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;


    private EditForm? Editform { get; set; } = new();
    private HostDto.Mutate Host { get; set; } = new();
    private async void HandleValidSubmit()
    {
        HostResponse.Create response = await HostService.CreateAsync(new HostRequest.Create
        {
            Host = Host
        });
        Navigation!.NavigateTo("host/" + response.HostId);
    }
}