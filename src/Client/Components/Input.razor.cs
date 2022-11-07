using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class Input
{
    private string _label = "Label: ";
    private bool _required = false;

    [Parameter]
    public string PlaceHolder { get; set; } = "";
    [Parameter]
    public InputType Type { get; set; } = InputType.TEXT;
    [Parameter]
    public List<string> Items { get; set; } = new();
    [Parameter]
    public string Label
    {
        get => _label;

        set => _label = $"{value}: ";
    }

    [Parameter]
    public string customCss { get; set; } = "";
    [Parameter]
    public bool Required { get; set; } = false;
    [Parameter]
    public int OptionalId { get; set; } = -1;
    [Parameter]
    public EventCallback<string> Action { get; set; } = new();
    //[Parameter] public string Label { get; set; } = "Label: ";
    public enum InputType
    {
        SELECT,
        TEXT
    }

    public string SelectedOption
    {
        get => _selectedOption;
        set
        {
            _selectedOption = value;
            Action.InvokeAsync(_selectedOption);
        }
    }

    private string _selectedOption = "";
}
