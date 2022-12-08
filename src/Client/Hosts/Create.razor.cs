using Domain.Accounts;
using Domain.Constants;
using Domain.VirtualMachines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Services.Processors;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Create
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;
    [Inject] public IProcessorService ProcessorService { get; set; } = default!;

    private EditForm? Editform { get; set; } = new();
    private HostDto.Mutate Host { get; set; } = new();

    List<ProcessorDto> Processors { get; set; } = new();
    int m { get; set; } = new();

    protected override async Task OnInitializedAsync() {
        ProcessorResponse.GetIndex processorResponse = await ProcessorService.GetIndexAsync(new ProcessorRequest.GetIndex {
          
        });
        Processors = processorResponse.Processors;
        m = Processors.Count();
    }

    private async void HandleValidSubmit()
    {
        HostResponse.Create response = await HostService.CreateAsync(new HostRequest.Create
        {
            Host = Host
        });
        Navigation!.NavigateTo("host/" + response.HostId);
    }
    private Dictionary<string, string> MakeProcessorItems() => Processors.ToDictionary(x => x.Name, x => x.Name);
    private void SetProcessorValue(string value) => Host.Name = "test";
}