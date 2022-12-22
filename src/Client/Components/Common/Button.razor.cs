using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class Button
{
    [Parameter]
    public string? Href { get; set; }

    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;

    [Parameter]
    public EventCallback OnClick { get; set; } = new();

    [Parameter]
    public bool InverseStyle { get; set; } = false;

    [Parameter]
    public bool Toggle { get; set; } = false;

    [Parameter]
    public bool IsEnabled { get; set; } = true;

    [Parameter]
    public string customCss { get; set; } = "";

    private async Task HandleClick()
    {
        if (OnClick.HasDelegate && IsEnabled)
        {
            await OnClick.InvokeAsync();
        }
    }

}