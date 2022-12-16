using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class PortButton
{
    [Parameter]
    public string Text { get; set; } = "Forgot text";
    [Parameter]
    public EventCallback<string> OnClick { get; set; } = new();
    [Parameter]
    public string customCss { get; set; } = "";
}