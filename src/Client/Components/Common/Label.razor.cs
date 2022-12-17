using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class Label
{
    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;
}