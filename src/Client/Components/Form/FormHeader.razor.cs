using Microsoft.AspNetCore.Components;

namespace Client.Components.Form;

public partial class FormHeader
{
    [Parameter, EditorRequired]
    public string Icon { get; set; } = default!;
    [Parameter, EditorRequired]
    public string Text { get; set; } = default!;
}