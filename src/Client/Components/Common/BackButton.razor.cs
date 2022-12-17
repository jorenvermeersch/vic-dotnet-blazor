using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class BackButton
{
    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; } = new();
}