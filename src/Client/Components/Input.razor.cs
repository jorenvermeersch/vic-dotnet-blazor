using Bogus.Bson;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;
using System.Linq.Expressions;

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
    [Parameter, EditorRequired] public Expression<Func<string>> For { get; set; } = default!;
    [Parameter] public string? Id { get; set; }
    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null;
        return true;
    }

    //@oninput="(ChangeEventArgs __e) => Action.InvokeAsync(string.Concat(this.OptionalId, __e?.Value?.ToString()))"
}


