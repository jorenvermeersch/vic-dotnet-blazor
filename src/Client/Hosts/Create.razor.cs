using Domain.Accounts;
using Domain.Constants;
using Domain.VirtualMachines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Services.Processors;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Create {
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;
    [Inject] public IProcessorService ProcessorService { get; set; } = default!;

    private EditForm? Editform { get; set; } = new();
    private HostDto.Mutate Host { get; set; } = new();
    public int VirtualisationFactor { get; set; } = default!;

    private List<ProcessorDto>? Processors { get; set; }
    public ProcessorDto SelectedProcessors { get; set; }
    public List<ProcessorDto>? ChosenProcessors { get; set; }
    public ProcessorVirtualisation? NewProcessor { get; set; }
    private string _customcss = "background-color: white";

    protected override async Task OnInitializedAsync() {
        ProcessorResponse.GetIndex processorResponse = await ProcessorService.GetIndexAsync(new ProcessorRequest.GetIndex {
            Amount = 5
        });
        Processors = processorResponse.Processors;
    }

    private async void HandleValidSubmit() {
        HostResponse.Create response = await HostService.CreateAsync(new HostRequest.Create {
            Host = Host
        });
        Navigation!.NavigateTo("host/" + response.HostId);
    }
    private Dictionary<string, string> MakeProcessorItems() => Processors.ToDictionary(x => x.Name.ToString(), x => JsonConvert.SerializeObject(x));
    private void SetProcessorValue(string value){
        NewProcessor.Processor = JsonConvert.DeserializeObject<ProcessorDto>(value)!;
    }

    private void AddProcessor() {
        Host.Specifications.Processors.Add(new KeyValuePair<ProcessorDto, int>(NewProcessor.Processor, NewProcessor.VirtualisationFactor));
    }

    public class ProcessorVirtualisation {
        public ProcessorDto Processor { get; set; }
        public int VirtualisationFactor { get; set; }

    }
}
