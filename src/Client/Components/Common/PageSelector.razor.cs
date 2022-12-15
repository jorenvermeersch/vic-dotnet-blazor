using Microsoft.AspNetCore.Components;

namespace Client.Components.Common;

public partial class PageSelector
{
    [Parameter]
    public int NumberOfPages { get; set; } = 5;

    [Parameter]
    public EventCallback<int> OnClick { get; set; }

    [Parameter]
    public int SelectedPage { get; set; } = 1;
}