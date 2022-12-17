using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class DataCard
{
    [Parameter, EditorRequired]
    public IDictionary<string, string> Entries { get; set; } = default!;

    [Parameter]
    public EventCallback OnClick { get; set; } = new();

    [Parameter]
    public string? HoverIcon { get; set; }

    [Parameter]
    public bool InverseStyle { get; set; } = false;

    [Parameter]
    public string customCss { get; set; } = "";

    private async Task HandleClickAsync()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

}