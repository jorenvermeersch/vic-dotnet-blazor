using Microsoft.AspNetCore.Components;

namespace Client.Components.Misc;
public partial class NoResultsMessage
{
    [Parameter, EditorRequired]
    public string Message { get; set; } = default!;

    [Parameter]
    public bool InverseStyle { get; set; } = false;
}