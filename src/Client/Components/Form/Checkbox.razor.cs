using Microsoft.AspNetCore.Components;

namespace Client.Components.Form;

public partial class Checkbox
{
    [Parameter, EditorRequired]
    public string Label { get; set; } = default!;
    private bool _value;
    [Parameter]
    public bool Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;
            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }
}