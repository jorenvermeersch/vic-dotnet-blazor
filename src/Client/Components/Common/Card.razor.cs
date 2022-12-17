using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class Card
{
    [Parameter, EditorRequired]
    public string Value { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Href { get; set; } = default!;
}