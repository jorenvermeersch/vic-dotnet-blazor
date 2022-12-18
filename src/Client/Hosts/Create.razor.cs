using FluentValidation;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Shared.Hosts;

namespace Client.Hosts;

public partial class Create
{
    public class ProcessorFactorPair
    {
        public ProcessorDto? Processor { get; set; }
        public int VirtualisationFactor { get; set; }

        public class ProcessorFactorPairValidator : AbstractValidator<ProcessorFactorPair>
        {
            public ProcessorFactorPairValidator()
            {
                RuleLevelCascadeMode = CascadeMode.Stop;

                RuleFor(x => x.Processor).NotEmpty().WithMessage("Type is verplicht.");
                RuleFor(x => x.VirtualisationFactor).GreaterThanOrEqualTo(1).WithMessage("Virtualisatiefactor moet groter of gelijk zijn aan 1.");
            }
        }

    }

    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IHostService HostService { get; set; } = default!;
    [Inject] public IProcessorService ProcessorService { get; set; } = default!;

    private HostDto.Mutate Host { get; set; } = new();

    private List<ProcessorDto>? Processors { get; set; }
    public ProcessorFactorPair ChosenProcessor { get; set; } = new();
    private Dictionary<string, string> chosenProcessorSpecifications = new();


    protected override async Task OnInitializedAsync()
    {
        ProcessorResponse.GetIndex response = await ProcessorService.GetIndexAsync(new ProcessorRequest.GetIndex
        {
            Amount = 5
        });
        Processors = response.Processors;
    }

    private async void HandleValidSubmit()
    {
        var request = new HostRequest.Create { Host = Host };
        HostResponse.Create response = await HostService.CreateAsync(request);
        Navigation.NavigateTo($"host/{response.HostId}");
    }
    private Dictionary<string, string> SerializeProcessorsForDropDown()
    {
        return Processors!.ToDictionary(x => x.Name.ToString(), x => JsonConvert.SerializeObject(x));
    }

    private void SetChosenProcessor(string value)
    {
        ChosenProcessor.Processor = JsonConvert.DeserializeObject<ProcessorDto>(value)!;
        chosenProcessorSpecifications = new()
        {
            { "Cores", ChosenProcessor.Processor.Cores.ToString() },
            { "Threads", ChosenProcessor.Processor.Threads.ToString() },
        };

    }

    private void AddProcessor()
    {
        if (ChosenProcessor.Processor is null || ChosenProcessor.VirtualisationFactor <= 0) return;

        Host.Specifications.Processors.Add(
            new KeyValuePair<ProcessorDto, int>(ChosenProcessor.Processor!, ChosenProcessor.VirtualisationFactor)
        );
        ChosenProcessor = new();
    }

    private void RemoveProcessor(KeyValuePair<ProcessorDto, int> processor)
    {
        Host.Specifications.Processors.Remove(processor);
    }
}
