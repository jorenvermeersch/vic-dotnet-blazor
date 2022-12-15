using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class DataCard
{
    [Parameter, EditorRequired]
    public IDictionary<string, string> Entries { get; set; } = new Dictionary<string, string>();

    [Parameter]
    public EventCallback<string> OnClick { get; set; } = new();

    [Parameter]
    public string customCss { get; set; } = "";

}