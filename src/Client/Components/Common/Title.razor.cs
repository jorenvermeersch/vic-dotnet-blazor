using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class Title
{
    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;
}