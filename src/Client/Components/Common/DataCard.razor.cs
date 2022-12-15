using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class DataCard
{
    [Parameter, EditorRequired]
    public IDictionary<string, string> Entries { get; set; } = default!;

    [Parameter]
    public EventCallback<string> OnClick { get; set; } = new();

    [Parameter]
    public string customCss { get; set; } = "";

}