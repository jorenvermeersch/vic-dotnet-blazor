using Client.SharedFiles.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Client.Components.Form;

public partial class AvailabilityOptionSelect
{
    [Inject]
    private IStringLocalizer<Resource> Localizer { get; set; } = default!;

    [Parameter]
    public string customCss { get; set; } = "";
    private string _value = "";
    [Parameter]
    public string Value
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
    public EventCallback<string> ValueChanged { get; set; }
}