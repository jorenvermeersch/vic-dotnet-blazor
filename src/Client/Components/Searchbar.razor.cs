using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class Searchbar
{
    private string? _searchterm;

    [Parameter, EditorRequired]
    public string PlaceHolder { get; set; } = default!;

    [Parameter]
    public string CustomCss { get; set; } = "";

    [Parameter]
    public EventCallback<string> SearchTermChanged { get; set; }

    [Parameter]
    public string? Searchterm { get; set; }


    protected override void OnParametersSet()
    {
        _searchterm = Searchterm;
    }

    private Task HandleSearchTermChanged(ChangeEventArgs args)
    {
        Searchterm = args.Value?.ToString();
        return SearchTermChanged.InvokeAsync(Searchterm);
    }
}