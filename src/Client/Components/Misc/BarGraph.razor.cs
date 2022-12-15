using Microsoft.AspNetCore.Components;

namespace Client.Components.Misc;

public partial class BarGraph
{
    [Parameter, EditorRequired]
    public IList<int> Data { get; set; } = new List<int>();

    [Parameter, EditorRequired]
    public int UpperBound { get; set; }

    [Parameter, EditorRequired]
    public string Title { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Unit { get; set; } = default!;

    [Parameter, EditorRequired]
    public string StartLabel { get; set; } = default!;

    [Parameter, EditorRequired]
    public string EndLabel { get; set; } = default!;
}