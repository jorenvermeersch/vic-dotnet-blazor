using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class Label
{
    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;
}